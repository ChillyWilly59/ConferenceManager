using ConferenceManager.DTO;

namespace ConferenceManager.Services
{
    public interface IApplicationService
    {
        Task CreateApplication(ApplicationDto applicationDto);
        Task UpdateApplication(Guid id, ApplicationDto applicationDto);
        Task DeleteApplication(Guid id);
        Task SubmitApplication(Guid id);
        Task<ApplicationDto> GetApplication(Guid id);
        Task<IEnumerable<ApplicationDto>> GetUnsignedApplications(Guid userId);
        Task<IEnumerable<ActivityDto>> GetActivities();
        Task<IEnumerable<ApplicationDto>> GetUnsubmittedApplicationsOlder(DateTime unsubmittedOlder);
        Task<IEnumerable<ApplicationDto>> GetApplicationsSubmittedAfter(DateTime unsubmittedOlder);
    }
}
