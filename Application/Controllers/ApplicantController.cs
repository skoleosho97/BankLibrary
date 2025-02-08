using Core.Dtos.Requests;
using Core.Dtos.Responses;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers
{
    [ApiController]
    [Route("/applicants")]
    [Produces("application/json")]
    public class ApplicantController : ControllerBase
    {
        private readonly IApplicantService service;

        public ApplicantController(IApplicantService service)
        {
            this.service = service;
        }

        /// <summary>
        /// Creates an applicant.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <response code="201">Returns created applicant</response>
        /// <response code="400">Returns if applicant data is invalid</response>
        /// <response code="409">Returns if applicant data conflicts with other applicant data</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> CreateApplicant([FromBody] CreateApplicantRequest request)
        {
            ApplicantResponse applicant = await service.CreateApplicant(request);

            return CreatedAtAction(nameof(GetApplicantById), new { id = applicant.Id }, applicant);
        }

        /// <summary>
        /// Gets all applicants, paginated.
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Returns paginated, or empty, list of applicants</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetApplicants()
        {
            IEnumerable<ApplicantResponse> applicants = await service.GetApplicants();

            return Ok(applicants);
        }

        /// <summary>
        /// Gets a specified applicant.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">Returns applicant</response>
        /// <response code="404">Returns if applicant was not found</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetApplicantById([FromRoute] int id)
        {
            ApplicantResponse applicant = await service.GetApplicantById(id);

            return Ok(applicant);
        }

        /// <summary>
        /// Updates an applicant.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <response code="204">Returns updated applicant</response>
        /// <response code="400">Returns if given applicant data is invalid</response>
        /// <response code="404">Returns if given applicant could not be found</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public Task<IActionResult> UpdateApplicant([FromRoute] int id, [FromBody] UpdateApplicantRequest request)
        {
            service.UpdateApplicant(id, request);

            return Task.FromResult<IActionResult>(NoContent());
        }

        /// <summary>
        /// Deletes an applicant.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="204">Returns if applicant was successfully deleted</response>
        /// <response code="404">Returns if given applicant could not be found</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public Task<IActionResult> DeleteApplicant([FromRoute] int id)
        {
            service.DeleteApplicant(id);

            return Task.FromResult<IActionResult>(NoContent());
        }
    }

}
