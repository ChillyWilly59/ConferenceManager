using Microsoft.AspNetCore.Mvc;
using ConferenceManager.Services;
using ConferenceManager.DTO;

namespace ConferenceManager.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ApplicationController : ControllerBase
    {
        private readonly IApplicationService _applicationService;

        public ApplicationController(IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        [HttpPost("/create")]
        public async Task<IActionResult> CreateApplication([FromBody] ApplicationDto applicationDto)
        {
            await _applicationService.CreateApplication(applicationDto);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateApplication(Guid id, [FromBody] ApplicationDto applicationDto)
        {
            await _applicationService.UpdateApplication(id, applicationDto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteApplication(Guid id)
        {
            await _applicationService.DeleteApplication(id);
            return Ok();
        }

        [HttpPost("{id}/submit")]
        public async Task<IActionResult> SubmitApplication(Guid id)
        {
            await _applicationService.SubmitApplication(id);
            return Ok();
        }

        [HttpGet("submittedAfter")]
        public async Task<IActionResult> GetApplicationsSubmittedAfter(DateTime submittedAfter)
        {
            var applications = await _applicationService.GetApplicationsSubmittedAfter(submittedAfter);
            return Ok(applications);
        }

        [HttpGet("unsubmittedOlder")]
        public async Task<IActionResult> GetUnsubmittedApplicationsOlderThan(DateTime unsubmittedOlder)
        {
            var applications = await _applicationService.GetUnsubmittedApplicationsOlder(unsubmittedOlder);
            return Ok(applications);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetApplication(Guid id)
        {
            var application = await _applicationService.GetApplication(id);
            return Ok(application);
        }
    }
}
