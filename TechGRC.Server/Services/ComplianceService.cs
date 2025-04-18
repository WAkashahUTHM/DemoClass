using Microsoft.EntityFrameworkCore;
using TechGRC.Server.Data;
using TechGRC.Server.Models.DTO;

namespace TechGRC.Server.Services
{
    public class ComplianceService : IComplianceService
    {
        private readonly TechGrcDbContext _context;

        public ComplianceService(TechGrcDbContext context)
        {
            _context = context;
        }

        // Get all Compliance Standards linked to a specific Production Process
        public async Task<ProductionProcessComplianceDTO> GetProductionProcessComplianceAsync(Guid productionProcessId)
        {
            var productionProcess = await _context.ProductionProcesses
                .Include(p => p.LinkedComplianceStandards)
                .FirstOrDefaultAsync(p => p.Id == productionProcessId);

            if (productionProcess == null)
            {
                throw new KeyNotFoundException("Production process not found.");
            }

            // Convert the Compliance Standards into DTOs
            var complianceDTOs = productionProcess.LinkedComplianceStandards.Select(cs => new ComplianceStandardDTO
            {
                ComplianceStandardId = cs.Id,
                StandardName = cs.StandardName,
                Description = cs.Description,
                LastChecked = cs.LastChecked,
                IsCompliant = cs.IsCompliant
            }).ToList();

            return new ProductionProcessComplianceDTO
            {
                ProductionProcessId = productionProcess.Id,
                ProcessName = productionProcess.Name,
                LinkedComplianceStandards = complianceDTOs
            };
        }

        // Update Compliance status of a Production Process
        public async Task UpdateComplianceStatusAsync(Guid productionProcessId, Guid complianceStandardId, bool isCompliant)
        {
            var productionProcess = await _context.ProductionProcesses
                .Include(p => p.LinkedComplianceStandards)
                .FirstOrDefaultAsync(p => p.Id == productionProcessId);

            if (productionProcess == null)
            {
                throw new KeyNotFoundException("Production process not found.");
            }

            var complianceStandard = await _context.ComplianceStandards
                .FirstOrDefaultAsync(cs => cs.Id == complianceStandardId);

            if (complianceStandard == null)
            {
                throw new KeyNotFoundException("Compliance standard not found.");
            }

            // Find the compliance standard in the production process
            var linkedCompliance = productionProcess.LinkedComplianceStandards
                .FirstOrDefault(cs => cs.Id == complianceStandardId);

            if (linkedCompliance == null)
            {
                productionProcess.LinkedComplianceStandards.Add(complianceStandard);
            }

            // Update the compliance status
            linkedCompliance.IsCompliant = isCompliant;
            await _context.SaveChangesAsync();
        }
    }

}
