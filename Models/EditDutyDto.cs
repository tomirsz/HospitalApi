using System.ComponentModel.DataAnnotations;


public class EditDutyDto
{
    [Required]
    public string Date { get; set; }

    public EditDutyDto(string date)
    {
        this.Date = date;
    }
}
