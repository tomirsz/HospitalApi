
public class UserDTO {

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Pesel { get; set; }
    public string Role { get; set; }

    public UserDTO(string firstName, string lastName, string pesel)
    {
        this.FirstName = firstName;
        this.LastName = lastName;
        this.Pesel = pesel;
    }
}