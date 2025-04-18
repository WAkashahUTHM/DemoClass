using Microsoft.AspNetCore.Mvc;
using TechGRC.Server.Services;

namespace TechGRC.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ComplianceController : ControllerBase
    {
        private readonly IComplianceService _complianceService;

        public ComplianceController(IComplianceService complianceService)
        {
            _complianceService = complianceService;
        }

        // Endpoint to get the compliance status of a production process
        [HttpGet("process/{productionProcessId}/compliance")]
        public async Task<IActionResult> GetComplianceForProcess(Guid productionProcessId)
        {
            try
            {
                var complianceDTO = await _complianceService.GetProductionProcessComplianceAsync(productionProcessId);
                return Ok(complianceDTO); // Return the compliance status as a DTO
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message); // Return 404 if the process is not found
            }
        }

        // Endpoint to update the compliance status for a specific compliance standard
        [HttpPut("process/{productionProcessId}/compliance/{complianceStandardId}")]
        public async Task<IActionResult> UpdateComplianceStatus(Guid productionProcessId, Guid complianceStandardId, [FromBody] bool isCompliant)
        {
            try
            {
                await _complianceService.UpdateComplianceStatusAsync(productionProcessId, complianceStandardId, isCompliant);
                return NoContent(); // Return 204 No Content on success
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message); // Return 404 if the production process or compliance standard is not found
            }
        }
    }

}
