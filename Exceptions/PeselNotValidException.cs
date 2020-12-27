using System;

public class PeselNotValidException : Exception
{
    public PeselNotValidException()
    {
    }

    public PeselNotValidException(string pesel) : base(String.Format("Pesel: {0} is not valid", pesel))
    {

    }
}
