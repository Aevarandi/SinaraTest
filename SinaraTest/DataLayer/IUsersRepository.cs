namespace SinaraTest.Data;

public interface IUsersRepository
{
    List<User> GetAllUsers();
    void SaveUser(User user);
    void DeleteUser(long userId);
    User GetUserById(long id);
}