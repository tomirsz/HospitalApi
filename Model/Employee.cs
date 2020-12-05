using System;
using System.Collections.Generic;

public class Employee : User
{
    private string firstName;
    private string lastName;
    private string pesel;
    private List<Duty> duty = new List<Duty>();

    public Employee(string username, string password, string firstName, string lastName, string pesel) : base(username, password)
    {
        this.firstName = firstName;
        this.lastName = lastName;
        this.pesel = pesel;
    }

    public string FirstName
    {
        get { return this.firstName; }
        set { this.firstName = value; }
    }

    public string LastName
    {
        get { return this.lastName; }
        set { this.lastName = value; }
    }

    public string Pesel
    {
        get { return this.pesel; }
        set { this.pesel = value; }
    }

    public List<Duty> Duty
    {
        get { return this.duty; }
    }

}