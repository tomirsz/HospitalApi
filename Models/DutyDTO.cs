public class DutyDTO
{

    public string Username { get; set; }
    public string Date { get; set; }
    public string Specialization { get; set; }

    public DutyDTO(string username, string date, string specialization)
    {
        this.Username = username;
        this.Date = date;
        this.Specialization = specialization;
    }
 
}
