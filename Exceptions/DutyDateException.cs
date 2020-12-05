using System;

[Serializable]
public class DutyDateException : Exception
{
    public DutyDateException()
    {
    }

    public DutyDateException(DateTime date) : base(String.Format("Duty date {0} does not match the schedule", date.Date.ToString()))
    {
    }
}