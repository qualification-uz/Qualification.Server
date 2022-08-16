using Microsoft.EntityFrameworkCore;
using Qualification.Domain.Entities.Users;

namespace Qualification.Data.Contexts;

public class AppDbContext : DbContext
{

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var con = "Server=127.0.0.1;Port=5432;Database=QualificationLocal;User Id=postgres;Password=root;";

        optionsBuilder.UseNpgsql(con);
    }

    public virtual DbSet<Teacher> Teachers { get; set; }
    public virtual DbSet<Student> Students { get; set; }
}