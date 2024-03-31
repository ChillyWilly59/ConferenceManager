using ConferenceManager.Data.DTO;
using ConferenceManager.Data.Models;

namespace ConferenceManager.Services
{
    public interface IApplicationRepository
    {
        Task Create(Application application);
        Task Update(Application application);
        Task Delete(Application application);
        Task<Application> GetById(Guid id);
        Task<IEnumerable<Application>> GetApplications(DateTime? submittedAfter, DateTime? unsubmittedOlder);
        Task<IEnumerable<Application>> GetUnsignedApplications(Guid userId);
        Task<IEnumerable<Application>> GetApplicationsSubmittedAfter(DateTime submittedAfter);
        Task<IEnumerable<Application>> GetUnsubmittedApplicationsOlder(DateTime unsubmittedOlder);
        Task <Application> GetUnsignedApplicationByAuthor(Guid authorId);
    }
}