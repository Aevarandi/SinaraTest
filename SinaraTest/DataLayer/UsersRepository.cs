using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using SinaraTest.DataLayer;

namespace SinaraTest.Data;

public class UsersRepository : IUsersRepository
{
    private readonly InMemoryContext context;


    public UsersRepository(InMemoryContext context)
    {
        this.context = context;
    }

    public User GetUserById(long id)
    {
        return context.Users.SingleOrDefault(x => x.Id == id && x.Deleted == false);
    }
    
    public List<User> GetAllUsers()
    {
        return context.Users.Where(x => x.Deleted == false).ToList();
    }
    
    public void SaveUser(User user)
    {
        Validator.ValidateObject(user, new ValidationContext(user), true);
        if (user.Id == 0)
            context.Users.Add(user);
        else
            context.Entry(user).State = EntityState.Modified;
        context.SaveChanges();
    }

    public void DeleteUser(long userId)
    {
        var user = context.Users.FirstOrDefault(x => x.Id == userId);
        if (user == null) return;
        user.Deleted = true;
        context.Entry(user).State = EntityState.Modified;
        context.SaveChanges();
    }
}