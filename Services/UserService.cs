using System;
using System.Collections.Generic;
using HospitalApi.Exceptions;
using HospitalApiModels;


public class UserService
{

    private static UserService instance;
    private readonly UserRepositoryImpl userRepository = UserRepositoryImpl.Instance();

    protected UserService() {
    }

    public static UserService Instance() {
        if (instance == null) {
            instance = new UserService();
        }

        return instance;
    }

    public User CreateUser(string username, string password, string firstName, string lastName, string pesel)
    {
        string role = "USER";
        if (username.Contains("admin") || username.Contains("ADMIN") || username.Contains("Admin")) {
            role = "ADMIN";
        }

        User user = new User(username, BCrypt.Net.BCrypt.HashPassword(password), firstName, lastName, pesel, role);
        CheckUsernameIsUnique(username);
        userRepository.CreateUser(user);
        return user;
    }

    public Nurse CreateNurse(string username, string password, string firstName, string lastName, string pesel)
    {
        Nurse user = new Nurse(username, BCrypt.Net.BCrypt.HashPassword(password), firstName, lastName, pesel, "NURSE");
        CheckUsernameIsUnique(username);
        userRepository.CreateNurse(user);
        return user; 
    }

    public Doctor CreateDoctor(string username, string password, string firstName, string lastName, string pesel, Specialization specialization, string pwz)
    {
        Doctor user = new Doctor(username, BCrypt.Net.BCrypt.HashPassword(password), firstName, lastName, pesel, "DOCTOR", specialization, pwz);
        CheckUsernameIsUnique(username);
        userRepository.CreateDoctor(user);
        return user;
    }

    public User Signup(string username, string password) {
        User user = userRepository.FindByUsername(username);
        if (user != null) {
            if (CheckPassword(user, password))
            {
                return user;
            }
            else {
                throw new AuthException();
            }
        }
        throw new AuthException();
    }

    private Boolean CheckPassword(User user, string password) {
        return BCrypt.Net.BCrypt.Verify(password, user.Password);
    }

    public List<Nurse> FindAllEmployees()
    {
        return userRepository.FindAllNurses(); ;
    }

    public User GetUser(string username) {
        User user = userRepository.FindByUsername(username);

        if (user != null)
        {
            return user;
        }
        else {
            throw new UserNotFoundException(username);
        }
    }

    public List<User> GetAllUsers()
    {
        return userRepository.GetAllUsers();
    }

    public List<Doctor> GetAllDoctors()
    {
        List<Doctor> doctors = new List<Doctor>();
        List<User> allUsers = GetAllUsers();
        foreach (User user in allUsers)
        {
            if (user.GetType() == typeof(Doctor))
            {
                doctors.Add((Doctor)user);
            }
        }
        return doctors;
    }

    private bool CheckUsernameIsUnique(string username)
    {
        User user = userRepository.FindByUsername(username);
        if (user != null)
        {
            throw new UserAlreadyExistsException(username);
        }
        else
        {
            return true;
        }
    }

    public User EditUser(UserDTO editedUser, string username) {

        User user = userRepository.FindByUsername(username);
        if (user == null) {
            throw new UserNotFoundException(username);
        }

        user.FirstName = editedUser.FirstName;
        user.LastName = editedUser.LastName;
        user.Pesel = editedUser.Pesel;
        user.Role = editedUser.Role;
        return userRepository.UpdateUser(user);
    }

    public void DeleteUser(string username) {

        User user = userRepository.FindByUsername(username);
        if (user == null)
        {
            throw new UserNotFoundException(username);
        }
        else
        {
            userRepository.DeleteUser(user);
        }
    }

}