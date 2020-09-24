using Domain.Model.AggregatesModel.CourseAggregate;
using Domain.Model.AggregatesModel.RateAggregate;
using Domain.Model.AggregatesModel.UserAggregate;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Contexts
{
    public class ProffyContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Connection> Connections { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<TeacherCourse> TeacherCourses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=Proffy;Trusted_Connection=True;MultipleActiveResultSets=true");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Connection>()
                .HasOne(u => u.Teacher)
                .WithMany(t => t.Connections);

            modelBuilder.Entity<TeacherCourse>()
                .HasOne(c => c.Course)
                .WithMany(t => t.TeacherCourses);

            modelBuilder.Entity<Schedule>()
                .HasOne(t => t.TeacherCourse)
                .WithMany(s => s.Schedules);
        }
    }
}
