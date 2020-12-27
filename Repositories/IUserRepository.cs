using System.Collections.Generic;

public interface IUserRepository
{
    User CreateUser(User user);

    User DeleteUser(User user);

    User UpdateUser(User user);

    User FindByUsername(string username);

    Nurse CreateNurse(Nurse nurse);

    Duty GetDutyById(int id);

    Nurse FindNurseByUsername(string username);
}
