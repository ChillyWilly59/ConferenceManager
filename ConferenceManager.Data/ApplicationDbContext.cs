using ConferenceManager.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ConferenceManager.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Application> Applications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Application>()
                .Property(a => a.Id)
                .IsRequired(); 

            modelBuilder.Entity<Application>()
                .Property(a => a.Activity)
                .IsRequired(); 

            modelBuilder.Entity<Application>()
                .Property(a => a.Name)
                .HasMaxLength(100)
                .IsRequired(); 

            modelBuilder.Entity<Application>()
                .Property(a => a.Description)
                .HasMaxLength(300); 

            modelBuilder.Entity<Application>()
                .Property(a => a.Outline)
                .HasMaxLength(1000)
                .IsRequired();
        }
    }
}
