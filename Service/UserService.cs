using System;
using System.Collections.Generic;
using System.Linq;

public class UserService
{

    private List<User> users = new List<User>();
    private static UserService instance;

    protected UserService() {
    }

    public static UserService Instance() {
        if (instance == null) {
            instance = new UserService();
        }

        return instance;
    }

    public List<User> Users
    {
        get { return this.users; }
        set { this.users = value; }
    }

    public User createUser(string username, string password)
    {
        User user = new User(username, BCrypt.Net.BCrypt.HashPassword(password));
        try
        {
            checkUsernameIsUnique(username);
            user.Role = "ADMIN";
            users.Add(user);
            return user;
        }
        catch (UserAlreadyExistsException e)
        {
            Console.WriteLine(e.Message);
            throw new UserAlreadyExistsException(username);
        }
    }

    public Employee createNurse(string username, string password, string firstName, string lastName, string pesel)
    {
        Employee user = new Nurse(username, BCrypt.Net.BCrypt.HashPassword(password), firstName, lastName, pesel);
        try
        {
            checkUsernameIsUnique(username);
            user.Role = "USER";
            users.Add(user);
            return user;
        }
        catch (UserAlreadyExistsException e)
        {
            Console.WriteLine(e.Message);
            throw new UserAlreadyExistsException(username);
        }
    }

    public Employee createDoctor(string username, string password, string firstName, string lastName, string pesel, Specialization specialization, string pwz)
    {
        Employee user = new Doctor(username, BCrypt.Net.BCrypt.HashPassword(password), firstName, lastName, pesel, specialization, pwz);
        try
        {
            checkUsernameIsUnique(username);
            user.Role = "USER";
            users.Add(user);
            return user;
        }
        catch (UserAlreadyExistsException e)
        {
            Console.WriteLine(e.Message);
            throw new UserAlreadyExistsException(username);
        }
    }

    public User Signup(string username, string password) {
        User user = this.users.Find(user => user.Username == username);
        if (user != null) {
            if (checkPassword(user, password))
            {
                return user;
            }
            else {
                throw new AuthException();
            }
        }
        throw new AuthException();
    }

    private Boolean checkPassword(User user, string password) {
        return BCrypt.Net.BCrypt.Verify(password, user.Password);
    }

    public User findUser(string username)
    {
        User user = this.users.Find(user => user.Username == username);
        return user;
    }

    public void showAllUsersForAdmin()
    {
        foreach (User user in users)
        {
            Console.WriteLine(user.ToString());
        }
    }

    public List<Employee> findAllEmployees()
    {
        List<Employee> resultList = new List<Employee>();
        foreach (User user in users)
        {
            if (user.GetType() != typeof(User))
            {
                resultList.Add((Employee)user);
            }
        }

        return resultList;
    }

    public List<User> getAllUsers()
    {
        return this.users;
    }

    public List<Nurse> getAllNurses()
    {
        List<Nurse> nurses = new List<Nurse>();
        List<User> allUsers = getAllUsers();
        foreach (User user in allUsers)
        {
            if (user.GetType() == typeof(Nurse))
            {
                nurses.Add((Nurse)user);
            }
        }
        return nurses;
    }

    public List<Doctor> getAllDoctors()
    {
        List<Doctor> doctors = new List<Doctor>();
        List<User> allUsers = getAllUsers();
        foreach (User user in allUsers)
        {
            if (user.GetType() == typeof(Doctor))
            {
                doctors.Add((Doctor)user);
            }
        }
        return doctors;
    }

    private bool checkUsernameIsUnique(string username)
    {
        List<User> users = this.users.Where(u => u.Username == username).ToList();
        if (users.Capacity > 0)
        {
            throw new UserAlreadyExistsException(username);
        }
        else
        {
            return true;
        }
    }

}