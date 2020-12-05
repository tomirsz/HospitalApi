using System;

[Serializable]
public class DutySpecializationException : Exception
{
    public DutySpecializationException()
    {
    }

    public DutySpecializationException(Specialization specialization) : base(String.Format("Doctor with {0} specialization already has a duty", SpecializationMap.getSpecialization(specialization)))
    {
    }
}