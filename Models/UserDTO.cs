
public class UserDTO {

    private string firstName;
    private string lastName;
    private string pesel;

    public UserDTO(string firstName, string lastName, string pesel)
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
}