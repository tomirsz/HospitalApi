using System.Collections.Generic;

public interface IUserRepository
{
    User CreateUser(User user);

    User deleteUser(User user);

    User updateUser(User user);

    User FindByUsername(string username);

    Nurse CreateNurse(Nurse nurse);

    Duty GetDutyById(int id);

    Nurse FindNurseByUsername(string username);
}
