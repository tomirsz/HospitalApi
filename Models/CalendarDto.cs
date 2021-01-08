using System;
using System.Collections.Generic;
using HospitalApiModels;

public class CalendarDto
{

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Username { get; set; }
    public Duty Duty { get; set; }
   
    public CalendarDto(string firstName, string lastName, string username, Duty duty)
    {
        this.FirstName = firstName;
        this.LastName = lastName;
        this.Username = username;
        this.Duty = duty;
        
    }
 }
