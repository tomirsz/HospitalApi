using System.ComponentModel.DataAnnotations.Schema;

[Table("doctors")]
public class Doctor : Nurse
{

    [Column("specialization")]
    public Specialization Specialization { get; set; }

    [Column("pwz")]
    public string Pwz { get; set; }

    public Doctor(string username, string password, string firstName, string lastName, string pesel, string role, Specialization specialization, string pwz) : base(username, password, firstName, lastName, pesel, role)
    {
        this.Specialization = specialization;
        this.Pwz = pwz;
    }
}