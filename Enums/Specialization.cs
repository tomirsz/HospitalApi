using System;
using System.Collections.Generic;

public enum Specialization
{
    INTERNIST,
    PEDIATRIST
}

static class SpecializationMap
{
    public static Dictionary<Specialization, string> specializationMap()
    {
        var specializations = new Dictionary<Specialization, string>();
        specializations.Add(Specialization.INTERNIST, "Internistyczna");
        specializations.Add(Specialization.PEDIATRIST, "Pediatryczna");
        return specializations;
    }

    public static string getSpecialization(Specialization specialization)
    {
        return specializationMap()[specialization];
    }
}