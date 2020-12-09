using System;
namespace HospitalApi.Exceptions
{
    [Serializable]
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException(string username) : base(String.Format("Cannot find {0} user.", username))
        {
        }
    }
}
