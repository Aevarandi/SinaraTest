using Microsoft.AspNetCore.Mvc;
using SinaraTest.Data;

namespace SinaraTest.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUsersRepository userRepository;
    private readonly IActiveDirectoryService activeDirectoryService;

    public UserController(IUsersRepository userRepository,
        IActiveDirectoryService activeDirectoryService)
    {
        this.userRepository = userRepository;
        this.activeDirectoryService = activeDirectoryService;
    }

    [HttpGet]
    public List<User> Get()
    {
        return userRepository.GetAllUsers();
    }

    [HttpGet("{id:long}")]
    public User Get(long id)
    {
        return userRepository.GetUserById(id);
    }

    [HttpPost]
    [Route("/saveUser")]
    public void SaveUser(User user)
    {
        if (!activeDirectoryService.IsUserExist(user.Username))
        {
            throw new Exception("Пользователь не зарегистрирован в AD");
        }
        userRepository.SaveUser(user);
    }

    [HttpDelete("{id:long}")]
    public void Delete(long id)
    {
        userRepository.DeleteUser(id);
    }
}