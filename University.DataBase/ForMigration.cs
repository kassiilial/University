using Entities.Entities;
using Microsoft.EntityFrameworkCore;

namespace Entities.Repositories
{
    public class ForMigration: DbContext
    {
        internal DbSet<University> Universities { get; set; }
        internal DbSet<Lection> Lections { get; set; }
        internal DbSet<Lector> Lectors { get; set; }
        internal DbSet<Student> Students { get; set; }
        internal DbSet<Homework> Homeworks { get; set; }
        internal DbSet<MarksAndVisited> MarksAndVisited { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=usersdb;Username=postgres;Password=12345");
        }
    }
}