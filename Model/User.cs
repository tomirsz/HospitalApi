using System;
using System.Collections.Generic;

public class User
{
    private string username;
    private string password;
    private string firstName;
    private string lastName;
    private string pesel;
    private string role;

    public User(string username, string password, string firstName, string lastName, string pesel, string role)
    {
        this.username = username;
        this.password = password;
        this.firstName = firstName;
        this.lastName = lastName;
        this.pesel = pesel;
        this.role = role;
    }

    public string Username
    {
        get { return this.username; }
        set { this.username = value; }
    }

    public string Password
    {
        get { return this.password; }
        set { this.password = value; }
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

    public string Role
    {
        get { return this.role; }
        set { this.role = value; }
    }
}