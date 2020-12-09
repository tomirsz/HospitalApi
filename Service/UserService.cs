using System;
using System.Collections.Generic;
using System.Linq;
using HospitalApi.Exceptions;

public class UserService
{

    private List<User> users;
    private static UserService instance;
    private readonly SerializeService serializeService = SerializeService.Instance();

    protected UserService() {
        this.users = serializeService.deserialize();
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
            serializeService.Serialize(this.users);
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
            serializeService.Serialize(this.users);
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
            serializeService.Serialize(this.users);
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

    public User editUser(UserDTO editedUser, string username) {

            User user = this.users.Find(user => user.Username == username);
        if (user == null) {
            throw new UserNotFoundException(username);
        }
            if (user.GetType() == typeof(User)) {
                user.Username = editedUser.Username;
                user.Password = BCrypt.Net.BCrypt.HashPassword(editedUser.Password);
                return user;
            } else if(user.GetType() == typeof(Nurse)) {
                Nurse nurse = getAllNurses().Find(user => user.Username == username);
                nurse.Username = editedUser.Username;
                nurse.Password = BCrypt.Net.BCrypt.HashPassword(editedUser.Password);
                nurse.FirstName = editedUser.FirstName;
                nurse.LastName = editedUser.LastName;
                nurse.Pesel = editedUser.Pesel;
                return nurse;
            } else {
                Doctor doctor = getAllDoctors().Find(user => user.Username == username);
                doctor.Username = editedUser.Username;
                doctor.Password = BCrypt.Net.BCrypt.HashPassword(editedUser.Password);
                doctor.FirstName = editedUser.FirstName;
                doctor.LastName = editedUser.LastName;
                doctor.Pesel = editedUser.Pesel;
                doctor.Specialization = (Specialization)Enum.Parse(typeof(Specialization), editedUser.Specialization.ToString());
                doctor.Pwz = editedUser.Pwz;
                return doctor;
            }
    }

}