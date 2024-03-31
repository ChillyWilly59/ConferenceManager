using ConferenceManager.Data;
using ConferenceManager.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ConferenceManager.Services
{
    public class ApplicationRepository(ApplicationDbContext context) : IApplicationRepository
    {
        public async Task Create(Application application)
        {
            context.Applications.Add(application);
            await context.SaveChangesAsync();
        }
        public async Task Update(Application application)
        {
            context.Applications.Update(application);
            await context.SaveChangesAsync();
        }
        public async Task Delete(Application application)
        {
            context.Applications.Remove(application);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Application>> GetApplications(DateTime? submittedAfter, DateTime? unsubmittedOlder)
        {
            IQueryable<Application> query = context.Applications;

            if (submittedAfter != null)
            {
                query = query.Where(a => a.SubmittedAt >= submittedAfter.Value);
            }

            if (unsubmittedOlder != null)
            {
                query = query.Where(a => a.SubmittedAt == null && a.CreatedAt <= unsubmittedOlder.Value);
            }

            return await query.ToListAsync();
        }
        public async Task<Application> GetById(Guid id)
        {
            return await context.Applications.FindAsync(id);
        }

        public async Task<IEnumerable<Application>> GetUnsignedApplications(Guid userId)
        {
            return await context.Applications
                .Where(a => a.Author == userId && a.SubmittedAt == null)
                .ToListAsync();
        }

        public async Task<IEnumerable<Application>> GetApplicationsSubmittedAfter(DateTime submittedAfter)
        {
            return await context.Applications
                .Where(a => a.SubmittedAt.HasValue && a.SubmittedAt >= submittedAfter)
                .ToListAsync();
        }

        public async Task<IEnumerable<Application>> GetUnsubmittedApplicationsOlder(DateTime unsubmittedOlder)
        {
            return await context.Applications
                .Where(a => !a.SubmittedAt.HasValue && a.CreatedAt <= unsubmittedOlder)
                .ToListAsync();
        }
    }
}