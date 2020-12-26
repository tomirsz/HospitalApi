using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("doctors")]
public class Doctor : Nurse
{

    [Key]
    [Column("id")]
    public long DoctorId { get; set; }


    [Column("specialization")]
    private Specialization specialization;

    [Column("pwz")]
    private string pwz;

    public Doctor(string username, string password, string firstName, string lastName, string pesel, string role, Specialization specialization, string pwz) : base(username, password, firstName, lastName, pesel, role)
    {
        this.specialization = specialization;
        this.pwz = pwz;
    }

    public Specialization Specialization
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