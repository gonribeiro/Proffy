using Microsoft.EntityFrameworkCore;
using Proffy.CourseMicroservice.Domain.AggregatesModel.CourseAggregate;

namespace Proffy.CourseMicroservice.Infrastructure.DataAccess.Contexts
{
    public class CourseContext : DbContext
    {
        public DbSet<TeacherCourse> TeacherCourses { get; set; }
        public DbSet<TeacherCourseSchedule> TeacherCourseSchedules { get; set; }
        public DbSet<Course> Courses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=Courses;Trusted_Connection=True;MultipleActiveResultSets=true");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TeacherCourseSchedule>()
                .HasOne(c => c.TeacherCourse)
                .WithMany(s => s.TeacherCourseSchedules);

            modelBuilder.Entity<TeacherCourse>()
                .HasOne(c => c.Course)
                .WithMany(t => t.TeacherCourses);
        }
    }
}
