using Microsoft.EntityFrameworkCore;

namespace WebApiPractice.Model
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options): base(options)
        {

        }
        public DbSet<Student> Students { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //send student table
            modelBuilder.Entity<Student>().HasData(
                new Student
                {
                    FirstName = "Johnson",
                    LastName = "Oyesina",
                    Gender = Gender.MALE,
                    Id = 1
                }

                );
            modelBuilder.Entity<Student>().HasData(
                new Student
                {
                    FirstName = "Omowunmi",
                    LastName = "Abigail",
                    Gender = Gender.FEMALE,
                    Id = 2
                }

                );
            modelBuilder.Entity<Student>().HasData(
                new Student
                {
                    FirstName = "Paul",
                    LastName = "Scholes",
                    Gender = Gender.OTHER,
                    Id = 3
                }

                );
        }
    }
}
