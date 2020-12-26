using System.Collections.Generic;

public interface IUserRepository
{
    User CreateUser(User user);

    User deleteUser(int id);

    User updateUser(User user);

    User FindByUsername(string username);

    Nurse CreateNurse(Nurse nurse);
}
