﻿using System;
using System.Collections.Generic;
using System.Linq;
using HospitalApi.Exceptions;
using HospitalApiModels;
using Microsoft.Extensions.Configuration;



public class DutyService
{
    private UserService userService = new UserService();
    private readonly UserRepositoryImpl userRepository = new UserRepositoryImpl();

    public DutyService()
    {
    }

    public bool AddDuty(Nurse nurse, Duty duty)
    {
        if (DateIsValid(nurse, duty))
        {
            if (SpecializationIsValid(nurse, duty))
            {
                nurse.duty.Add(duty);
                userRepository.UpdateNurse(nurse);
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
        if (DateIsValid(user, newDuty)) {

            if (SpecializationIsValid(user, newDuty)) {
             
                duty.Date = DateTime.Parse(dto.Date);
                user.duty.Add(duty);
                userRepository.UpdateNurse(user);
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
        userRepository.UpdateNurse(user);

    }

    public List<CalendarDto> GetDutiesFromCurrentMonth(int month, int year) {
        return userRepository.GetDutiesFromCurrentMonth(month, year);
    }

    public List<CalendarDto> GetDutiesFromCurrentDay(string date)
    {
        return userRepository.GetDutiesFromCurrentDay(date);
    }

    private bool DateIsValid(Nurse nurse, Duty duty)
    {
        List<Duty> employeDuties = nurse.duty;
        if (employeDuties.Capacity <= 0)
        {
            return true;
        }

        return CheckDuties(employeDuties, duty);
    }

    private bool CheckDuties(List<Duty> employeeDuties, Duty duty)
    {

        if (CheckNumberOfDutiesInCurrentMonth(employeeDuties, duty))
        {
            return false;
        }

        foreach (Duty employeeCurrentDuty in employeeDuties)
        {
            if (CompareDate(employeeCurrentDuty.Date, duty))
                {
                    return false;
                }
            }
        return true;
    }

    private bool CheckNumberOfDutiesInCurrentMonth(List<Duty> employeeDuties, Duty duty)
    {
        List<Duty> result = employeeDuties.Where(d => d.Date.Month == duty.Date.Month).ToList();
        int resultSize = result.Count;
        if (resultSize >= 10)
        {
            return true;
        }
        return false;
    }

    private bool CompareDate(DateTime date, Duty duty)
    {
        return date == duty.Date || date == duty.Date.AddDays(1) || date == duty.Date.AddDays(-1);
    }

    private bool SpecializationIsValid(Nurse nurse, Duty duty)
    {
        if (duty.Specialization != Specialization.NURSE && nurse.GetType() == typeof(Doctor))
        {
            return CheckSpecialization(duty);
        }
        else
        {
            return true;
        }

    }

    //Better to fetch only duties instead doctors
    private bool CheckSpecialization(Duty duty)
    {
        List<Doctor> result = userService.GetAllDoctors().Where(
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