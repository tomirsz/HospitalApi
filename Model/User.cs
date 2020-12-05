public class User
{
    private string username;
    private string password;
    private string role;

    public User(string username, string password)
    {
        this.username = username;
        this.password = password;
    }

    public string Username
    {
        get { return this.username; }
        set { this.username = value; }
    }

    public string Password
    {
        get { return this.password; }
        set { this.password = value; }
    }

    public string Role
    {
        get { return this.role; }
        set { this.role = value; }
    }
}