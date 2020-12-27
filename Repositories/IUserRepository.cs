using System.Collections.Generic;

public interface IUserRepository
{
    User CreateUser(User user);

    User UpdateUser(User user);

    User DeleteUser(User user);

    User FindByUsername(string username);

    Nurse CreateNurse(Nurse nurse);

    Nurse UpdateNurse(Nurse nurse);

    Doctor CreateDoctor(Doctor doctor);

    Duty GetDutyById(int id);

    List<Nurse> FindAllNurses();

    List<User> GetAllUsers();

    Nurse FindNurseByUsername(string username);
}
