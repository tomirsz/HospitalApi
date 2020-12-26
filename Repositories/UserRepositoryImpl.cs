using System.Collections.Generic;
using System.Linq;

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

    public User deleteUser(int id)
    {
        throw new System.NotImplementedException();
    }

    public User updateUser(User user)
    {
        var result = userContext.Update(user);
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

    public Duty CreateDuty(Duty duty) {
        var result = userContext.Add(duty);
        userContext.SaveChanges();
        return result.Entity;
    }

    public List<Nurse> FindAllNurses()
    {
        var result = userContext.Nurse.ToList();
        return result;
    }

    public Nurse updateNurse(Nurse nurse) {
        var result = userContext.Update(nurse);
        userContext.SaveChanges();
        return result.Entity;
    }

}
