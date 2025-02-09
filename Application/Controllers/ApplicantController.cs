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
        /// <response code="201">The applicant was created successfully.</response>
        /// <response code="400">One or more applicant data was invalid.</response>
        /// <response code="409">One or more applicant data conflicts with existing applicant data.</response>
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
        /// Gets a specified applicant.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">The requested applicant was found.</response>
        /// <response code="404">The requested applicant does not exist or was not found.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetApplicantById([FromRoute] int id)
        {
            ApplicantResponse applicant = await service.GetApplicantById(id);

            return Ok(applicant);
        }

        /// <summary>
        /// Gets all applicants, paginated.
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Applicants were retrieved successfully. If an empty list is returned, no applicants currently exist.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetApplicants()
        {
            IEnumerable<ApplicantResponse> applicants = await service.GetApplicants();

            return Ok(applicants);
        }

        /// <summary>
        /// Updates an applicant.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <response code="204">The applicant was successfully updated.</response>
        /// <response code="400">One or more applicant data was invalid.</response>
        /// <response code="404">The requested applicant does not exist or was not found.</response>
        /// <response code="409">One or more applicant data conflicts with existing applicant data.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
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
        /// <response code="204">The applicant was successfully deleted.</response>
        /// <response code="404">The requested applicant does not exist or was not found.</response>
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
