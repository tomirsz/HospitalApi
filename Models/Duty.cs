using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("duty")]
public class Duty
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Column("date")]
    public DateTime Date { get; set; }

    [Column("specialization")]
    public Specialization Specialization { get; set; }

    [Column("nurseid")]
    public long NurseId { get; set; }

    public Duty(DateTime date) {
        this.Date = date;
        this.Specialization = Specialization.NURSE;
    }

    public Duty(DateTime date, Specialization specialization)
    {
        this.Date = date;
        this.Specialization = specialization;
    }
}