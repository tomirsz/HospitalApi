using System;
public class Duty
{
    private DateTime date;
    private Specialization specialization;

    public Duty(DateTime date, Specialization specialization)
    {
        this.date = date;
        this.specialization = specialization;
    }

    public DateTime Date
    {
        get { return this.date; }
        set { this.date = value; }
    }

    public Specialization Specialization
    {
        get { return this.specialization; }
        set { this.specialization = value; }
    }

    public override string ToString()
    {
        return this.date.ToString();
    }
}