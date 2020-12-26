using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HospitalApi.Exceptions;

public class DutyService
{
    private UserService userService = UserService.Instance();
    private readonly UserRepositoryImpl userRepository = UserRepositoryImpl.Instance();
    private static DutyService instance;

    protected DutyService()
    {
    }

    public static DutyService Instance()
    {
        if (instance == null)
        {
            instance = new DutyService();
        }

        return instance;
    }

    public bool addDuty(Nurse nurse, Duty duty)
    {
        if (dateIsValid(nurse, duty))
        {
            if (specializationIsValid(nurse, duty))
            {
                nurse.duty.Add(duty);
                userRepository.updateNurse(nurse);
                return true;
            }
            throw new DutySpecializationException(duty.Specialization);
        }
        else
        {
            throw new DutyDateException(duty.Date);
        }
    }

    public Duty FindById(int id)
    {
        Duty duty = userRepository.GetDutyById(id);
        return duty;
    }

    public bool EditDuty(EditDutyDto dto, string username, int id) {
        Nurse user = userRepository.FindNurseByUsername(username);
        Duty duty = userRepository.GetDutyById(id);
        Duty newDuty = new Duty(DateTime.Parse(dto.Date), duty.Specialization);
        if (user == null) {
            throw new UserNotFoundException(user.Username);
        }
        if (duty == null) {
            throw new DutyNotFoundException(duty.Id);
        }
        user.duty.Remove(duty);
        if (dateIsValid(user, newDuty)) {

            if (specializationIsValid(user, newDuty)) {
             
                duty.Date = DateTime.Parse(dto.Date);
                user.duty.Add(duty);
                userRepository.updateNurse(user);
                return true;
            }
            throw new DutySpecializationException(duty.Specialization);
        }
        else
        {
            throw new DutyDateException(DateTime.Parse(dto.Date));
        }       
    }

    public void DeleteDuty(string username, int id)
    {
        Nurse user = userRepository.FindNurseByUsername(username);
        Duty duty = userRepository.GetDutyById(id);
        if (user == null)
        {
            throw new UserNotFoundException(user.Username);
        }
        if (duty == null)
        {
            throw new DutyNotFoundException(duty.Id);
        }

        user.duty.Remove(duty);
        userRepository.updateNurse(user);

    }

    private bool dateIsValid(Nurse nurse, Duty duty)
    {
        List<Duty> employeDuties = nurse.duty;
        if (employeDuties.Capacity <= 0)
        {
            return true;
        }

        return checkDuties(employeDuties, duty);
    }

    private bool checkDuties(List<Duty> employeeDuties, Duty duty)
    {

        if (checkNumberOfDutiesInCurrentMonth(employeeDuties, duty))
        {
            return false;
        }

        foreach (Duty employeeCurrentDuty in employeeDuties)
        {
            if (compareDate(employeeCurrentDuty.Date, duty))
                {
                    return false;
                }
            }
        return true;
    }

    private bool checkNumberOfDutiesInCurrentMonth(List<Duty> employeeDuties, Duty duty)
    {
        List<Duty> result = employeeDuties.Where(d => d.Date.Month == duty.Date.Month).ToList();
        int resultSize = result.Count;
        if (resultSize >= 2)
        {
            return true;
        }
        return false;
    }

    private bool compareDate(DateTime date, Duty duty)
    {
        return date == duty.Date || date == duty.Date.AddDays(1) || date == duty.Date.AddDays(-1);
    }

    private bool specializationIsValid(Nurse nurse, Duty duty)
    {
        if (duty.Specialization != Specialization.NURSE && nurse.GetType() == typeof(Doctor))
        {
            return checkSpecialization(duty);
        }
        else
        {
            return true;
        }

    }

    private bool checkSpecialization(Duty duty)
    {
        List<Doctor> result = userService.getAllDoctors().Where(
            doc => doc.duty.Any(
                d => d.Specialization == duty.Specialization && d.Date.Date == duty.Date.Date)).ToList();
        if (result.Capacity > 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}