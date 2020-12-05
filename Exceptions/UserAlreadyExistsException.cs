using System;

[Serializable]
public class UserAlreadyExistsException : Exception
{
    public UserAlreadyExistsException()
    {
    }

    public UserAlreadyExistsException(string username) : base(String.Format("User {0} already exists", username))
    {

    }
}