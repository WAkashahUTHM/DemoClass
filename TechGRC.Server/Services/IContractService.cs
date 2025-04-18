using TechGRC.Server.Models.DTO;

namespace TechGRC.Server.Services
{
    public interface IContractService
    {
        Task<List<ContractDTO>> GetAllContractsAsync();
        Task<ContractDTO> GetContractByIdAsync(Guid contractId);
        Task<ContractDTO> CreateContractAsync(CreateContractDTO createContractDTO);
    }
}
