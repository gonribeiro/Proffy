using Microsoft.EntityFrameworkCore;
using Proffy.UserMicroservice.Domain.AggregatesModel.UserAggregate;

namespace Proffy.UserMicroservice.Infrastructure.DataAccess.Contexts
{
    public class UserContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Connection> Connections { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=Users;Trusted_Connection=True;MultipleActiveResultSets=true");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Connection>()
                .HasOne(u => u.User)
                .WithMany(c => c.Connections);
        }
    }
}