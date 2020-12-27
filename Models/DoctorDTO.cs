using System;
using System.ComponentModel.DataAnnotations;

public class DoctorDTO
{
    [StringLength(30, MinimumLength = 3)]
    public string Username { get; set; }

    [StringLength(30, MinimumLength = 6)]
    public string Password { get; set; }

    [StringLength(30, MinimumLength = 3)]
    public string FirstName { get; set; }

    [StringLength(30, MinimumLength = 3)]
    public string LastName { get; set; }

    [StringLength(11, MinimumLength = 11)]
    public string Pesel { get; set; }

    [Required]
    public string Specialization { get; set; }

    [Required]
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
