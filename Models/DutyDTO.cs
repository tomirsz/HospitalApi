using System.ComponentModel.DataAnnotations;

public class DutyDTO
{
    [StringLength(30, MinimumLength = 3)]
    public string Username { get; set; }

    [Required]
    public string Date { get; set; }

    public string Specialization { get; set; }

    public DutyDTO(string username, string date, string specialization)
    {
        this.Username = username;
        this.Date = date;
        this.Specialization = specialization;
    }
 
}
