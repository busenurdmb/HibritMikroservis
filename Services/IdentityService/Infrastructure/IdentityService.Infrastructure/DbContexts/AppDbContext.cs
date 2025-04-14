using IdentityService.Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace IdentityService.Infrastructure.DbContexts;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<User> Users => Set<User>();
}
