using System;
using System.Collections.Generic;
using System.Linq;
using HospitalApi.Exceptions;

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
        User user = new User(username, BCrypt.Net.BCrypt.HashPassword(password), firstName, lastName, pesel, "ADMIN");
        try
        {
            CheckUsernameIsUnique(username);
            userRepository.CreateUser(user);
            return user;
        }
        catch (UserAlreadyExistsException)
        {
            throw new UserAlreadyExistsException(username);
    }
}

    public Nurse CreateNurse(string username, string password, string firstName, string lastName, string pesel)
    {
        Nurse user = new Nurse(username, BCrypt.Net.BCrypt.HashPassword(password), firstName, lastName, pesel, "NURSE");
        try
        {
            CheckUsernameIsUnique(username);
            userRepository.CreateNurse(user);
            return user;
        }
        catch (UserAlreadyExistsException)
        {
            throw new UserAlreadyExistsException(username);
        }
    }

    public Doctor CreateDoctor(string username, string password, string firstName, string lastName, string pesel, Specialization specialization, string pwz)
    {
        Doctor user = new Doctor(username, BCrypt.Net.BCrypt.HashPassword(password), firstName, lastName, pesel, "DOCTOR", specialization, pwz);
        try
        {
            CheckUsernameIsUnique(username);
            userRepository.CreateDoctor(user);
            return user;
        }
        catch (UserAlreadyExistsException)
        {
            throw new UserAlreadyExistsException(username);
        }
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

    public List<Nurse> GetAllNurses()
    {
        List<Nurse> nurses = new List<Nurse>();
        List<User> allUsers = GetAllUsers();
        foreach (User user in allUsers)
        {
            if (user.GetType() == typeof(Nurse))
            {
                nurses.Add((Nurse)user);
            }
        }
        return nurses;
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
        return userRepository.UpdateUser(user);

        //if (editedUser.Specialization == null)
        //{
        //    user.FirstName = editedUser.FirstName;
        //    user.LastName = editedUser.LastName;
        //    user.Pesel = editedUser.Pesel;
        //    serializeService.Serialize(this.users);
        //    return user;
        //}
        //else if (editedUser.Specialization == "NURSE")
        //{
        //    Nurse nurse = getAllNurses().Find(user => user.Username == username);
        //    nurse.FirstName = editedUser.FirstName;
        //    nurse.LastName = editedUser.LastName;
        //    nurse.Pesel = editedUser.Pesel;
        //    serializeService.Serialize(this.users);
        //    return nurse;
        //}else {
        //    Doctor doctor = getAllDoctors().Find(user => user.Username == username);
        //    doctor.FirstName = editedUser.FirstName;
        //    doctor.LastName = editedUser.LastName;
        //    doctor.Pesel = editedUser.Pesel;
        //    doctor.Specialization = (Specialization)Enum.Parse(typeof(Specialization), editedUser.Specialization.ToString());
        //    doctor.Pwz = editedUser.Pwz;
        //    serializeService.Serialize(this.users);
        //    return doctor;
        //}
        
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