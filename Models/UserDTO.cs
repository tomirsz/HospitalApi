
public class UserDTO {

    private string firstName;
    private string lastName;
    private string pesel;
    private string specialization;
    private string pwz;

    public UserDTO(string firstName, string lastName, string pesel, string specialization, string pwz)
    {
        this.firstName = firstName;
        this.lastName = lastName;
        this.pesel = pesel;
        this.specialization = specialization;
        this.pwz = pwz;
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