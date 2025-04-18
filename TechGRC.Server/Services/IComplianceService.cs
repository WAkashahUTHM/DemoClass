using TechGRC.Server.Models.DTO;

namespace TechGRC.Server.Services
{
    public interface IComplianceService
    {
        Task<ProductionProcessComplianceDTO> GetProductionProcessComplianceAsync(Guid productionProcessId);
        Task UpdateComplianceStatusAsync(Guid productionProcessId, Guid complianceStandardId, bool isCompliant);
    }

}
