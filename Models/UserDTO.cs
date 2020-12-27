using System.ComponentModel.DataAnnotations;

public class UserDTO {

    [StringLength(30, MinimumLength = 3)]
    public string FirstName { get; set; }

    [StringLength(30, MinimumLength = 3)]
    public string LastName { get; set; }

    [StringLength(11, MinimumLength = 11)]
    public string Pesel { get; set; }

    [Required]
    public string Role { get; set; }

    public UserDTO(string firstName, string lastName, string pesel)
    {
        this.FirstName = firstName;
        this.LastName = lastName;
        this.Pesel = pesel;
    }
}