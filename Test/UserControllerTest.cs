using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using SinaraTest;
using SinaraTest.Controllers;
using SinaraTest.Data;
using SinaraTest.DataLayer;

namespace Test;

public class UserControllerTest
{
    private readonly InMemoryContext context;
    private readonly IUsersRepository usersRepository;
    private readonly UserController userController;

    public UserControllerTest()
    {
        var services = new ServiceCollection();
        services.AddDbContext<InMemoryContext>(x => x.UseInMemoryDatabase("Test"));
        services.AddTransient<IUsersRepository, UsersRepository>();
        services.AddTransient<IActiveDirectoryService, ActiveDirectoryService>();
        services.AddScoped<UserController>();
        var serviceProvider = services.BuildServiceProvider();
        context = serviceProvider.GetService<InMemoryContext>();
        userController = serviceProvider.GetService<UserController>();
        var scope = serviceProvider.CreateScope();
        usersRepository = scope.ServiceProvider.GetService<IUsersRepository>();
    }
    
    [Test]
    public void GetUserList()
    {
        Assert.That(context.Users, Is.Empty);
        var user1 = CreateUser();
        var user2 = CreateUser(x =>
        {
            x.Name = "Вася";
            x.Username = "Sinara\\vasya";
        });

        var users = userController.Get();
        Assert.That(users.Count, Is.EqualTo(2));
        Assert.That(users.Single(x => x.Id == user1.Id
                                      && x.Deleted == user1.Deleted
                                      && x.Name == user1.Name
                                      && x.Surname == user1.Surname
                                      && x.Username == user1.Username
        ), !Is.Null);

        Assert.That(users.Single(x => x.Id == user2.Id
                                      && x.Deleted == user2.Deleted
                                      && x.Name == user2.Name
                                      && x.Surname == user2.Surname
                                      && x.Username == user2.Username
        ), !Is.Null);
    }

    [Test]
    public void UserData()
    {
        var user = CreateUser();
        var userFromController = userController.Get(user.Id);

        Assert.That(userFromController, !Is.Null);
        Assert.That(userFromController.Deleted, Is.EqualTo(user.Deleted));
        Assert.That(userFromController.Id, Is.EqualTo(user.Id));
        Assert.That(userFromController.Name, Is.EqualTo(user.Name));
        Assert.That(userFromController.Patronymic, Is.EqualTo(user.Patronymic));
        Assert.That(userFromController.Surname, Is.EqualTo(user.Surname));
        Assert.That(userFromController.Username, Is.EqualTo(user.Username));
    }

    [Test]
    public void UserDelete()
    {
        var user = CreateUser();
        userController.Delete(user.Id);
        Assert.That(context.Users.Single(x => x.Id == user.Id).Deleted, Is.True);
    }

    [Test]
    public void UserEdit()
    {
        var user = CreateUser();
        user.Surname = "Петрович";
        userController.SaveUser(user);
        Assert.That(context.Users.Single(x => x.Id == user.Id).Surname, Is.EqualTo("Петрович"));
    }
    
    [Test]
    public void UserEditWrongDomain()
    {
        var user = CreateUser(x =>x.Username = "skn\\petr");
        Assert.Throws<Exception>(delegate { userController.SaveUser(user); });
    }

    private User CreateUser(Action<User> modifier = null)
    {
        var user = new User
        {
            Name = "Иван",
            Surname = "Иванов",
            Patronymic = "Иванович",
            Username = "Sinara\\IIVanov",
        };
        modifier?.Invoke(user);
        usersRepository.SaveUser(user);
        return user;
    }
}