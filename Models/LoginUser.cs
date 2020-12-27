using System;
using System.ComponentModel.DataAnnotations;


namespace HospitalApi.Models
{
    public class LoginUser
    {
        [StringLength(30, MinimumLength = 3)]
        public string Username { get; set; }

        [StringLength(30, MinimumLength = 6)]
        public string Password { get; set; }

        public LoginUser(string username, string password)
        {
            this.Username = username;
            this.Password = password;
        }
    }
}
