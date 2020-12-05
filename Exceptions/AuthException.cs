using System;

[Serializable]
public class AuthException : Exception
{
    public AuthException() : base(String.Format("Wrong username or password"))
    {
    }
}