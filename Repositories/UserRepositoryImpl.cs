using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using HospitalApiModels;
using System;
using System.Globalization;

public class UserRepositoryImpl : IUserRepository
{

    private readonly UserContext userContext;

    public UserRepositoryImpl()
    {
        //this.userContext = new UserContext("User ID = hospitalapi;Password=hospitalapi;Host=localhost;Port=5432;Database=hospitalapi;Integrated Security=true; Pooling=true;");

        //For docker use this
        this.userContext = new UserContext("Username=hospitalapi;Password=hospitalapi;Host=hospitalapi;Database=hospitalapi;");

    }

    public User CreateUser(User user)
    {
        var result = userContext.Add(user);
        userContext.SaveChanges();
        return result.Entity;
    }


    public User UpdateUser(User user)
    {
        var result = userContext.Update(user);
        userContext.SaveChanges();
        return result.Entity;
    }

    public User DeleteUser(User user)
    {
        var result = userContext.Remove(user);
        userContext.SaveChanges();
        return result.Entity;
    }

    public User FindByUsername(string username) {
        return userContext.Users.Where(u => u.Username.Equals(username)).FirstOrDefault();
    }  

    public Nurse CreateNurse(Nurse nurse) {
        var result = userContext.Add(nurse);
        userContext.SaveChanges();
        return result.Entity;
    }

    public Nurse UpdateNurse(Nurse nurse)
    {
        var result = userContext.Update(nurse);
        userContext.SaveChanges();
        return result.Entity;
    }

    public Doctor CreateDoctor(Doctor doctor) {
        var result = userContext.Add(doctor);
        userContext.SaveChanges();
        return result.Entity;
    }

    public Duty CreateDuty(Duty duty) {
        var result = userContext.Add(duty);
        userContext.SaveChanges();
        return result.Entity;
    }

    public Duty GetDutyById(int id) {
        var result = userContext.Duty.SingleOrDefault(d => d.Id == id);
        return result;
    }

    public List<Nurse> FindAllNurses()
    {
        return userContext.Nurse.Include(d => d.duty).ToList();
    }

    public List<User> GetAllUsers()
    {
        //Don't know how it works, but need it to fetch Duties for nurses and doctors
        userContext.Nurse.Include(d => d.duty).ToList();
        userContext.Doctor.Include(d => d.duty).ToList();
        return userContext.Users.ToList();
    }

    public Nurse FindNurseByUsername(string username) {
        return userContext.Nurse.Where(u => u.Username.Equals(username)).Include(d => d.duty).FirstOrDefault();
    }

    public List<CalendarDto> GetDutiesFromCurrentMonth(int month, int year) {
        List<Nurse> users = userContext.Nurse.Include(d => d.duty).ToList();
        List<CalendarDto> calendarDtos = new List<CalendarDto>();
        foreach (Nurse nurse in users) {
            if (nurse.duty.Capacity > 0) {
               List<Duty> result = nurse.duty.FindAll(d => d.Date.Month == month && d.Date.Year == year).ToList();
                foreach (Duty d in result) {
                    calendarDtos.Add(new CalendarDto(nurse.FirstName, nurse.LastName, nurse.Username, d));
                }
            }
        }
        return calendarDtos;
    }

    public List<CalendarDto> GetDutiesFromCurrentDay(string date)
    {
        List<Nurse> users = userContext.Nurse.Include(d => d.duty).ToList();
        List<CalendarDto> calendarDtos = new List<CalendarDto>();
        foreach (Nurse nurse in users)
        {
            if (nurse.duty.Capacity > 0)
            {
                List<Duty> result = nurse.duty.FindAll(d => d.Date.ToString("yyyy-MM-dd").Equals(date)).ToList();
                foreach (Duty d in result)
                {
                    calendarDtos.Add(new CalendarDto(nurse.FirstName, nurse.LastName, nurse.Username, d));
                }
            }
        }
        return calendarDtos;
    }
}
