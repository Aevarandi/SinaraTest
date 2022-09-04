using Microsoft.EntityFrameworkCore;
using SinaraTest.Data;

namespace SinaraTest.DataLayer;

public class InMemoryContext: DbContext
{
    public InMemoryContext(DbContextOptions<InMemoryContext> options) : base(options)
    {
    }
    public DbSet<User> Users { get; set;}
}