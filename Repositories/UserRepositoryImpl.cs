using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

public class UserRepositoryImpl : IUserRepository
{

    private readonly UserContext userContext;
    private static UserRepositoryImpl instance;


    protected UserRepositoryImpl(UserContext userContext)
    {
        this.userContext = userContext;
    }

    public static UserRepositoryImpl Instance() {
        if (instance == null) {
            instance = new UserRepositoryImpl(new UserContext("User ID = hospitalapi;Password=hospitalapi;Host=localhost;Port=5432;Database=hospitalapi;Integrated Security=true; Pooling=true;"));
        }

        return instance;
    }

    public User CreateUser(User user)
    {
        var result = userContext.Add(user);
        userContext.SaveChanges();
        return result.Entity;
    }


    public User updateUser(User user)
    {
        var result = userContext.Update(user);
        userContext.SaveChanges();
        return result.Entity;
    }

    public User deleteUser(User user)
    {
        var result = userContext.Remove(user);
        userContext.SaveChanges();
        return result.Entity;
    }

    public User FindByUsername(string username) {
        return userContext.Users.Where(u => u.Username.Equals(username)).FirstOrDefault();
    }

    public Nurse CreateNurse(Nurse nurse) {
        var result = userContext.Add(nurse);
        userContext.SaveChanges();
        return result.Entity;
    }

    public Nurse updateNurse(Nurse nurse)
    {
        var result = userContext.Update(nurse);
        userContext.SaveChanges();
        return result.Entity;
    }

    public Doctor CreateDoctor(Doctor doctor) {
        var result = userContext.Add(doctor);
        userContext.SaveChanges();
        return result.Entity;
    }

    public Nurse updateDoctor(Doctor doctor)
    {
        var result = userContext.Update(doctor);
        userContext.SaveChanges();
        return result.Entity;
    }

    public Duty CreateDuty(Duty duty) {
        var result = userContext.Add(duty);
        userContext.SaveChanges();
        return result.Entity;
    }

    public Duty GetDutyById(int id) {
        var result = userContext.Duty.SingleOrDefault(d => d.Id == id);
        return result;
    }

    public Duty SaveDuty(Duty duty) {
        var result = userContext.Add(duty);
        userContext.SaveChanges();
        return result.Entity;
    }

    public List<Nurse> FindAllNurses()
    {
        return userContext.Nurse.Include(d => d.duty).ToList();
    }

    public List<User> GetAllUsers()
    {
        //Don't know how it works, but need it to fetch Duties for nurses and doctors
        userContext.Nurse.Include(d => d.duty).ToList();
        userContext.Doctor.Include(d => d.duty).ToList();
        return userContext.Users.ToList();
    }

    public Nurse FindNurseByUsername(string username) {
        return userContext.Nurse.Where(u => u.Username.Equals(username)).FirstOrDefault();
    }
}
