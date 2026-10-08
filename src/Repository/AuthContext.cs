using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.ModelsConfig;

namespace Repository;

public class AuthContext(DbContextOptions<AuthContext> options) : DbContext(options)
{
    public DbSet<User> Users => base.Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.ApplyConfiguration(new UserConfig());

        base.OnModelCreating(modelBuilder);
    }
}
