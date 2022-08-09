using Microsoft.EntityFrameworkCore;
using Qualification.Domain.Entities.Users;

namespace Qualification.Data.Contexts;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
    {
            
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
    }
    
    public virtual DbSet<User> Users { get; set; }
}