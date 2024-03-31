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
    }
}
