using Core.Dtos.Requests;
using Core.Dtos.Responses;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers
{
    [ApiController]
    [Route("/applications")]
    [Produces("application/json")]
    public class ApplicationController : ControllerBase
    {
        private readonly IApplicationService service;

        public ApplicationController(IApplicationService service)
        {
            this.service = service;
        }

        /// <summary>
        /// Creates an application to apply for membership.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <response code="201">The application successfully processed, please check its status.</response>
        /// <response code="400">The application process failed, please review its contents.</response>
        /// <response code="404">One or more applicants associated with the application do not exist.</response>
        /// <response code="409">One or more applicants associated with the application conflict with one another.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Apply([FromBody] CreateApplicationRequest request)
        {
            ApplyResponse response = await service.Apply(request);

            return CreatedAtAction(nameof(GetApplicationById), new { id = response.Id }, response);
        }

        /// <summary>
        /// Get an application by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">The requested application was found.</response>
        /// <response code="404">The requested application does not exist or was not found.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetApplicationById([FromRoute] int id)
        {
            ApplicationResponse application = await service.GetApplicationResponseById(id);

            return Ok(application);
        }

        /// <summary>
        /// Get all applications, paginated.
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Applications were retrieved successfully. If an empty list is returned, no applications currently exist.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetApplications()
        {
            IEnumerable<ApplicationResponse> applications = await service.GetAllApplications();

            return Ok(applications);
        }
    }
}
