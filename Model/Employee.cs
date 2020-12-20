using System;
using System.Collections.Generic;

public class Employee : User
{
    private List<Duty> duty = new List<Duty>();

    public Employee(string username, string password, string firstName, string lastName, string pesel, string role) : base(username, password, firstName, lastName, pesel, role)
    {
    }

    public List<Duty> Duty
    {
        get { return this.duty; }
    }

}