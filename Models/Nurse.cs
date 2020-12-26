using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("nurse")]
public class Nurse : User
{
    [Column("duty")]
    public List<Duty> duty { get; set; }

    public Nurse(string username, string password, string firstName, string lastName, string pesel, string role) : base(username, password, firstName, lastName, pesel, role)
    {
        this.duty = new List<Duty>();
    }

}