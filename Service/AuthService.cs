using System;

public class AuthService
{

    private UserService userService;

    public AuthService(UserService userService)
    {
        this.userService = userService;
    }

    public bool loginUser(string username, string password)
    {
        try
        {
            User user = userService.findUser(username);
            if (user != null)
            {
                return verifyPassword(user, password);
            }
        }
        catch (AuthException ex)
        {
            Console.WriteLine(ex.Message);
        }
        return false;
    }

    private bool verifyPassword(User user, string password)
    {
        if (BCrypt.Net.BCrypt.Verify(password, user.Password))
        {
            return true;
        }
        else
        {
            throw new AuthException();
        }
    }

}