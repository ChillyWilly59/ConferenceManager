using Microsoft.AspNetCore.Mvc;
using ConferenceManager.Data.DTO;
using ConferenceManager.Services;

namespace ConferenceManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationController : ControllerBase
    {
        private readonly IApplicationService _applicationService;

        public ApplicationController(IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateApplication([FromBody] ApplicationDto applicationDto)
        {
            try
            {
                await _applicationService.CreateApplication(applicationDto);
                return Ok();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        [HttpPut("{id}/update")]
        public async Task<IActionResult> UpdateApplication(Guid id, [FromBody] ApplicationDto applicationDto)
        {
            try
            {
                await _applicationService.UpdateApplication(id, applicationDto);
                return Ok();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        [HttpDelete("{id}/delete")]
        public async Task<IActionResult> DeleteApplication(Guid id)
        {
            try
            {
                await _applicationService.DeleteApplication(id);
                return Ok();
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        [HttpPost("{id}/submit")]
        public async Task<IActionResult> SubmitApplication(Guid id)
        {
            try
            {
                await _applicationService.SubmitApplication(id);
                return Ok();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        [HttpGet("submitted-after")]
        public async Task<IActionResult> GetApplicationsSubmittedAfter(DateTime submittedAfter)
        {
            try
            {
                var applications = await _applicationService.GetApplicationsSubmittedAfter(submittedAfter);
                return Ok(applications);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        [HttpGet("unsubmitted-older")]
        public async Task<IActionResult> GetUnsubmittedApplicationsOlderThan(DateTime unsubmittedOlder)
        {
            try
            {
                var applications = await _applicationService.GetUnsubmittedApplicationsOlder(unsubmittedOlder);
                return Ok(applications);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetApplication(Guid id)
        {
            try
            {
                var application = await _applicationService.GetApplication(id);
                return Ok(application);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        [HttpGet("unsubmitted")]
        public async Task<IActionResult> GetUnsignedApplications(Guid userId)
        {
            try
            {
                var application = await _applicationService.GetUnsignedApplications(userId);
                return Ok(application);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        [HttpGet("activities")]
        public async Task<IActionResult> GetActivities()
        {
            try
            {
                var activities = await _applicationService.GetActivities();
                return Ok(activities);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }
    }
}
