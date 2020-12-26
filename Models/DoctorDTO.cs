using System;

public class DoctorDTO
{
    private string username;
    private string password;
    private string firstName;
    private string lastName;
    private string pesel;
    private string specialization;
    private string pwz;

    public DoctorDTO(string username, string password, string firstName, string lastName, string pesel, string specialization, string pwz) {
        this.username = username;
        this.password = password;
        this.firstName = firstName;
        this.lastName = lastName;
        this.pesel = pesel;
        this.specialization = specialization;
        this.pwz = pwz;
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

    public string Specialization
    {
        get { return this.specialization; }
        set { this.specialization = value; }
    }

    public string Pwz
    {
        get { return this.pwz; }
        set { this.pwz = value; }
    }


}
