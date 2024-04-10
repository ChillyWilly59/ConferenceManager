using ConferenceManager.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConferenceManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnsubmittedController : ControllerBase
    {
        private readonly IApplicationService _applicationService;

        public UnsubmittedController(IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }


        [HttpGet("unsubmitted")]
        public async Task<IActionResult> GetUnsignedApplications(Guid userId)
        {
            var application = await _applicationService.GetUnsignedApplications(userId);
            return Ok(application);
        }
    }
}
