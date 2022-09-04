namespace SinaraTest.Data;

public class UserService
{
    private readonly IUsersRepository usersRepository;

    public UserService(IUsersRepository usersRepository)
    {
        this.usersRepository = usersRepository;
    }

    public User GetUserById(long id)
    {
        return usersRepository.GetUserById(id);
    }

    public User[] GetUsers()
    {
        return usersRepository.GetAllUsers().ToArray();
    }

    public void SaveUser(User user)
    {
        usersRepository.SaveUser(user);
    }

    public void DeleteUser(long id)
    {
        usersRepository.DeleteUser(id);
    }
}