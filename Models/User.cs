using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("users")]
public class User
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Column("username")]
    public string Username { get; set; }

    [Column("password")]
    public string Password { get; set; }

    [Column("firstname")]
    public string FirstName { get; set; }

    [Column("lastname")]
    public string LastName { get; set; }

    [Column("pesel")]
    public string Pesel { get; set; }

    [Column("role")]
    public string Role { get; set; }

    //[Column("discriminator")]
    //public string Discriminator { get; set; }

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