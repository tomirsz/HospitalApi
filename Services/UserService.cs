﻿using System;
using System.Collections.Generic;
using System.Linq;
using HospitalApi.Exceptions;

public class UserService
{

    private List<User> users;
    private static UserService instance;
    private readonly SerializeService serializeService = SerializeService.Instance();
    private readonly UserRepositoryImpl userRepository = UserRepositoryImpl.Instance();

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

    public User createUser(string username, string password, string firstName, string lastName, string pesel)
    {
        User user = new User(username, BCrypt.Net.BCrypt.HashPassword(password), firstName, lastName, pesel, "ADMIN");
        try
        {
            checkUsernameIsUnique(username);
            userRepository.CreateUser(user);
            return user;
        }
        catch (UserAlreadyExistsException e)
        {
            Console.WriteLine(e.Message);
            throw new UserAlreadyExistsException(username);
        }
    }

    public Nurse createNurse(string username, string password, string firstName, string lastName, string pesel)
    {
        Nurse user = new Nurse(username, BCrypt.Net.BCrypt.HashPassword(password), firstName, lastName, pesel, "NURSE");
        try
        {
            checkUsernameIsUnique(username);
            userRepository.CreateNurse(user);
            return user;
        }
        catch (UserAlreadyExistsException e)
        {
            Console.WriteLine(e.Message);
            throw new UserAlreadyExistsException(username);
        }
    }

    public Doctor createDoctor(string username, string password, string firstName, string lastName, string pesel, Specialization specialization, string pwz)
    {
        Doctor user = new Doctor(username, BCrypt.Net.BCrypt.HashPassword(password), firstName, lastName, pesel, "DOCTOR", specialization, pwz);
        try
        {
            checkUsernameIsUnique(username);
            userRepository.CreateDoctor(user);
            return user;
        }
        catch (UserAlreadyExistsException e)
        {
            Console.WriteLine(e.Message);
            throw new UserAlreadyExistsException(username);
        }
    }

    public User Signup(string username, string password) {
        User user = userRepository.FindByUsername(username);
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

    public List<Nurse> findAllEmployees()
    {
        List<Nurse> results = userRepository.FindAllNurses();
        foreach (User user in users) {
            if (user.GetType() != typeof(User))
            {
                results.Add((Nurse)user);
            }
        }
        return results;
     
    }

    public User getUser(string username) {
        User user = userRepository.FindByUsername(username);

        if (user != null)
        {
            return user;
        }
        else {
            throw new UserNotFoundException(username);
        }
    }

    public List<User> getAllUsers()
    {
        return userRepository.GetAllUsers();
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

    public User editUser(UserDTO editedUser, string username) {

        User user = userRepository.FindByUsername(username);
        if (user == null) {
            throw new UserNotFoundException(username);
        }

        user.FirstName = editedUser.FirstName;
        user.LastName = editedUser.LastName;
        user.Pesel = editedUser.Pesel;
        return userRepository.updateUser(user);

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

    public void deleteUser(string username) {

        User user = userRepository.FindByUsername(username);
        if (user == null)
        {
            throw new UserNotFoundException(username);
        }
        else
        {
            userRepository.deleteUser(user);
        }
    }

}