using System;

public class DoctorDTO
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Pesel { get; set; }
    public string Specialization { get; set; }
    public string Pwz { get; set; }

    public DoctorDTO(string username, string password, string firstName, string lastName, string pesel, string specialization, string pwz) {
        this.Username = username;
        this.Password = password;
        this.FirstName = firstName;
        this.LastName = lastName;
        this.Pesel = pesel;
        this.Specialization = specialization;
        this.Pwz = pwz;
    }

}
