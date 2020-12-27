﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("users")]
public class User
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [StringLength(30, MinimumLength = 3)]
    [Column("username")]
    public string Username { get; set; }

    [StringLength(30, MinimumLength = 6)]
    [Column("password")]
    public string Password { get; set; }

    [StringLength(30, MinimumLength = 3)]
    [Column("firstname")]
    public string FirstName { get; set; }

    [StringLength(30, MinimumLength = 3)]
    [Column("lastname")]
    public string LastName { get; set; }

    [StringLength(11, MinimumLength = 11)]
    [Column("pesel")]
    public string Pesel { get; set; }

    [Column("role")]
    public string Role { get; set; }

    public User(string username, string password, string firstName, string lastName, string pesel, string role)
    {
        this.Username = username;
        this.Password = password;
        this.FirstName = firstName;
        this.LastName = lastName;
        this.Pesel = pesel;
        this.Role = role;
    }
}