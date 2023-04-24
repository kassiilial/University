using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Entities.Repositories
{
    public class AppContext : DbContext
    {
        private readonly ILogger<AppContext> _logger;
        internal DbSet<University> Universities { get; set; }
        internal DbSet<Lection> Lections { get; set; }
        internal DbSet<Lector> Lectors { get; set; }
        internal DbSet<Student> Students { get; set; }
        internal DbSet<Homework> Homeworks { get; set; }
        internal DbSet<MarksAndVisited> MarksAndVisited { get; set; }

        public AppContext(DbContextOptions<AppContext> options, ILogger<AppContext> logger):base(options)
        {
            _logger = logger;
            Database.EnsureDeleted();
            Database.EnsureCreated();
            _logger.LogInformation("Create new Database");
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<University>().HasData(
                new University[] 
                {
                    new University{Name = "SPbGU", Id = 1},
                    new University{Name = "ITMO", Id = 2}
                });
            modelBuilder.Entity<Lector>().HasData(
                new Lector[]
                {
                    new Lector{Name = "BobLector", UniversityId = 1, Id = 1},
                    new Lector{Name = "JohnLector", UniversityId = 2, Id = 2},
                    new Lector{Name = "BobLector", UniversityId = 2, Id = 3}
                }
            );
            modelBuilder.Entity<Lection>().HasData(
                new Lection[]
                {
                    new Lection{Name = "Maths", LectorId = 1, Id = 1},
                    new Lection{Name = "Biology", LectorId = 2, Id = 2},
                    new Lection{Name = "Maths1", LectorId = 3, Id = 3},
                    new Lection{Name = "Maths2", LectorId = 3, Id = 4},
                    new Lection{Name = "Maths3", LectorId = 3, Id = 5},
                    new Lection{Name = "Maths4", LectorId = 3, Id = 6}
                }
            );
            modelBuilder.Entity<Student>().HasData(
                new Student[]
                {
                    new Student{Name = "MarthaStudent", UniversityId = 1, Id = 1},
                    new Student{Name = "RonStudent", UniversityId = 1, Id = 2},
                    new Student{Name = "JohnStudent", UniversityId = 2, Id = 3},
                    new Student{Name = "AlexStudent", UniversityId = 2, Id = 4},
                    new Student{Name = "FluStudent", UniversityId = 2, Id = 5}
                }
            );
            modelBuilder.Entity<Homework>().HasData(
                new Homework[]
                {
                    new Homework{Name = "MathsHomework", LectionId = 1, Id = 1},
                    new Homework{Name = "BioHomework", LectionId = 2, Id = 2},
                    new Homework{Name = "MathsHomework1", LectionId = 3, Id = 3},
                    new Homework{Name = "MathsHomework2", LectionId = 4, Id = 4},
                    new Homework{Name = "MathsHomework3", LectionId = 5, Id = 5},
                    new Homework{Name = "MathsHomework4", LectionId = 6, Id = 6}

                });
            modelBuilder.Entity<MarksAndVisited>().HasData(
                new MarksAndVisited[]
                {
                    new MarksAndVisited{Id = 1, HomeworkId = 3, StudentId = 3, Mark = 3, IsVisited = false},
                    new MarksAndVisited{Id = 2, HomeworkId = 4, StudentId = 3, Mark = 3, IsVisited = false},
                    new MarksAndVisited{Id = 3, HomeworkId = 5, StudentId = 3, Mark = 3, IsVisited = false},
                    new MarksAndVisited{Id = 4, HomeworkId = 6, StudentId = 3, Mark = 5, IsVisited = true},
                   
                    new MarksAndVisited{Id = 5, HomeworkId = 3, StudentId = 4, Mark = 5, IsVisited = false},
                    new MarksAndVisited{Id = 6, HomeworkId = 4, StudentId = 4, Mark = 5, IsVisited = false},
                    new MarksAndVisited{Id = 7, HomeworkId = 5, StudentId = 4, Mark = 4, IsVisited = false},
                    new MarksAndVisited{Id = 8, HomeworkId = 6, StudentId = 4, Mark = 5, IsVisited = true},
                }
            );
        }

    }
}