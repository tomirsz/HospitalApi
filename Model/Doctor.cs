using System.Collections.Generic;
public class Doctor : Employee
{

    private Specialization specialization;
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

    public override string ToString()
    {
        string result = this.FirstName + " " + this.LastName + " doctor " + SpecializationMap.getSpecialization(this.specialization) + " ";

        if (this.Duty.Capacity > 0)
        {
            foreach (Duty d in this.Duty)
            {
                result = result + d.ToString() + " ";
            }
        }
        else
        {
            result = result + "No duties found";
        }
        return result;
    }
}