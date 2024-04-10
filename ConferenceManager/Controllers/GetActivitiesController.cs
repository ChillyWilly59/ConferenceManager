using ConferenceManager.Services;
using Microsoft.AspNetCore.Mvc;

namespace ConferenceManager.Controllers
{
    public class GetActivitiesController : ControllerBase
    {
        private readonly IApplicationService _applicationService;

        public GetActivitiesController(IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        [HttpGet("activities")]
        public async Task<IActionResult> GetActivities()
        {
            var activities = await _applicationService.GetActivities();
            return Ok(activities);
        }
    }
}
