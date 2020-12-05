﻿using System;
using System.Collections.Generic;
using System.Linq;

public class DutyService
{
    private UserService userService = UserService.Instance();
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

    public bool addDuty(Employee employee, Duty duty)
    {
        if (dateIsValid(employee, duty))
        {
            if (specializationIsValid(employee, duty))
            {
                employee.Duty.Add(duty);
                return true;
            }
            return false;
        }
        else
        {
            return false;
        }
    }

    private bool dateIsValid(Employee employee, Duty duty)
    {
        List<Duty> employeDuties = employee.Duty;
        if (employeDuties.Capacity <= 0)
        {
            return true;
        }

        return checkDuties(employeDuties, duty);
    }

    private bool checkDuties(List<Duty> employeeDuties, Duty duty)
    {
        foreach (Duty employeeCurrentDuty in employeeDuties)
        {
            if (compareDate(employeeCurrentDuty.Date, duty))
            {
                return false;
            }
        }
        return true;
    }

    private bool compareDate(DateTime date, Duty duty)
    {
        return date == duty.Date || date == duty.Date.AddDays(1) || date == duty.Date.AddDays(-1);
    }

    private bool specializationIsValid(Employee employee, Duty duty)
    {
        if (employee.GetType() == typeof(Doctor))
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
            doc => doc.Duty.Any(
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