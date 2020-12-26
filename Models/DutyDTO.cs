public class DutyDTO
{

    private string username;
    private string date;
    private string specialization;

    public DutyDTO(string username, string date, string specialization)
    {
        this.username = username;
        this.date = date;
        this.specialization = specialization;
    }


    public string Username
    {
        get { return this.username; }
        set { this.username = value; }
    }

    public string Date
    {
        get { return this.date; }
        set { this.date = value; }
    }

    public string Specialization
    {
        get { return this.specialization; }
        set { this.specialization = value; }
    }
  
}
