using Microsoft.AspNetCore.Mvc;
using TechGRC.Server.Models.DTO;
using TechGRC.Server.Services;

namespace TechGRC.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContractsController : ControllerBase
    {
        private readonly IContractService _contractService;

        public ContractsController(IContractService contractService)
        {
            _contractService = contractService;
        }

        // Endpoint zum Abrufen aller Verträge
        [HttpGet]
        public async Task<IActionResult> GetAllContracts()
        {
            try
            {
                var contracts = await _contractService.GetAllContractsAsync();
                return Ok(contracts);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Endpoint zum Abrufen eines bestimmten Vertrags
        [HttpGet("{contractId}")]
        public async Task<IActionResult> GetContractById(Guid contractId)
        {
            try
            {
                var contract = await _contractService.GetContractByIdAsync(contractId);
                return Ok(contract);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        // Endpoint zum Erstellen eines neuen Vertrags
        [HttpPost]
        public async Task<IActionResult> CreateContract([FromBody] CreateContractDTO createContractDTO)
        {
            try
            {
                var newContract = await _contractService.CreateContractAsync(createContractDTO);
                return CreatedAtAction(nameof(GetContractById), new { contractId = newContract.ContractId }, newContract);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

}
