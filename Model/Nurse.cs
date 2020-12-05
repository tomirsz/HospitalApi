﻿public class Nurse : Employee
{

    public Nurse(string username, string password, string firstName, string lastName, string pesel) : base(username, password, firstName, lastName, pesel)
    {

    }

    public override string ToString()
    {
        string result = this.FirstName + " " + this.LastName + " nurse ";

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