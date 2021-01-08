using System;
using System.Collections.Generic;
using HospitalApi.Exceptions;
using HospitalApiModels;
using System.Linq;

public class UserService
{

    private readonly UserRepositoryImpl userRepository = new UserRepositoryImpl();

    public UserService() {
    }

    public User CreateUser(string username, string password, string firstName, string lastName, string pesel)
    {
        string role = "USER";
        if (username.Contains("admin") || username.Contains("ADMIN") || username.Contains("Admin")) {
            role = "ADMIN";
        }

        User user = new User(username, BCrypt.Net.BCrypt.HashPassword(password), firstName, lastName, pesel, role);
        CheckUsernameIsUnique(username);
        if (!CheckPeselNumber(pesel)) {
            throw new PeselNotValidException(pesel);
        }
        userRepository.CreateUser(user);
        return user;
    }

    public Nurse CreateNurse(string username, string password, string firstName, string lastName, string pesel)
    {
        Nurse user = new Nurse(username, BCrypt.Net.BCrypt.HashPassword(password), firstName, lastName, pesel, "NURSE");
        CheckUsernameIsUnique(username);
        if (!CheckPeselNumber(pesel))
        {
            throw new PeselNotValidException(pesel);
        }
        userRepository.CreateNurse(user);
        return user; 
    }

    public Doctor CreateDoctor(string username, string password, string firstName, string lastName, string pesel, Specialization specialization, string pwz)
    {
        Doctor user = new Doctor(username, BCrypt.Net.BCrypt.HashPassword(password), firstName, lastName, pesel, "DOCTOR", specialization, pwz);
        CheckUsernameIsUnique(username);
        if (!CheckPeselNumber(pesel))
        {
            throw new PeselNotValidException(pesel);
        }
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

    public User EditUser(UserDTO editedUser, string username) {

        User user = userRepository.FindByUsername(username);
        if (user == null) {
            throw new UserNotFoundException(username);
        }
        if (!CheckPeselNumber(editedUser.Pesel))
        {
            throw new PeselNotValidException(editedUser.Pesel);
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

    private Boolean CheckPassword(User user, string password)
    {
        return BCrypt.Net.BCrypt.Verify(password, user.Password);
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

    private static int[] weights = { 9, 7, 3, 1, 9, 7, 3, 1, 9, 7 };

    public static bool CheckPeselNumber(string pesel)
    {
        if (IsEmptyOrNotProperLength(pesel))
            return false;

        if (!IsNumber(pesel))
            return false;

        if (!IsChecksumValid(pesel))
            return false;

        if (!HasValidDay(pesel))
            return false;

        return true;
    }

    private static bool HasValidDay(string pesel)
    {
        var day = int.Parse(pesel.Substring(4, 2));

        return 1 <= day && day <= 31;
    }

    private static bool IsEmptyOrNotProperLength(string pesel)
    {
        return string.IsNullOrWhiteSpace(pesel) || pesel.Length != 11;
    }

    private static bool IsNumber(string pesel)
    {
        return pesel.All(Char.IsDigit);
    }

    private static bool IsChecksumValid(string pesel)
    {
        int checksum = CalculateChecksum(pesel);

        return pesel.Last().ToString() == checksum.ToString();
    }

    private static int CalculateChecksum(string pesel)
    {
        int sum = 0;

        for (int i = 0; i < 10; i++)
            sum += weights[i] * int.Parse(pesel[i].ToString());

        return sum % 10;
    }
}
