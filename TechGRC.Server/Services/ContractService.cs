using Microsoft.EntityFrameworkCore;
using TechGRC.Server.Data;
using TechGRC.Server.Models;
using TechGRC.Server.Models.DTO;

namespace TechGRC.Server.Services
{

    public class ContractService : IContractService
    {
        private readonly TechGrcDbContext _context;

        public ContractService(TechGrcDbContext context)
        {
            _context = context;
        }

        public async Task<List<ContractDTO>> GetAllContractsAsync()
        {
            var contracts = await _context.SupplyContracts
                .Include(c => c.SupplierPartner)
                .Select(c => new ContractDTO
                {
                    ContractId = c.Id.ToString(),
                    Title = c.ContractTitle,
                    Description = c.Description,
                    StartDate = c.StartDate.ToString("yyyy-MM-dd"),
                    EndDate = c.EndDate.ToString("yyyy-MM-dd"),
                    Supplier = new SupplierDTO
                    {
                        SupplierId = c.SupplierPartner.Id.ToString(),
                        Name = c.SupplierPartner.Name,
                        Category = c.SupplierPartner.Category,
                        ContactInfo = c.SupplierPartner.ContactInfo
                    }
                }).ToListAsync();

            return contracts;
        }

        public async Task<ContractDTO> GetContractByIdAsync(Guid contractId)
        {
            var contract = await _context.SupplyContracts
                .Include(c => c.SupplierPartner)
                .Where(c => c.Id == contractId)
                .Select(c => new ContractDTO
                {
                    ContractId = c.Id.ToString(),
                    Title = c.ContractTitle,
                    Description = c.Description,
                    StartDate = c.StartDate.ToString("yyyy-MM-dd"),
                    EndDate = c.EndDate.ToString("yyyy-MM-dd"),
                    Supplier = new SupplierDTO
                    {
                        SupplierId = c.SupplierPartner.Id.ToString(),
                        Name = c.SupplierPartner.Name,
                        Category = c.SupplierPartner.Category,
                        ContactInfo = c.SupplierPartner.ContactInfo
                    }
                }).FirstOrDefaultAsync();

            if (contract == null)
            {
                throw new KeyNotFoundException("Contract not found.");
            }

            return contract;
        }

        public async Task<ContractDTO> CreateContractAsync(CreateContractDTO createContractDTO)
        {
            var supplier = await _context.SupplierPartners
                .FirstOrDefaultAsync(s => s.Id == createContractDTO.SupplierId);

            if (supplier == null)
            {
                throw new KeyNotFoundException("Supplier not found.");
            }

            var newContract = new SupplyContract
            {
                ContractTitle = createContractDTO.Title,
                Description = createContractDTO.Description,
                StartDate = createContractDTO.StartDate,
                EndDate = createContractDTO.EndDate,
                SupplierPartnerId = supplier.Id
            };

            _context.SupplyContracts.Add(newContract);
            await _context.SaveChangesAsync();

            return new ContractDTO
            {
                ContractId = newContract.Id.ToString(),
                Title = newContract.ContractTitle,
                Description = newContract.Description,
                StartDate = newContract.StartDate.ToString("yyyy-MM-dd"),
                EndDate = newContract.EndDate.ToString("yyyy-MM-dd"),
                Supplier = new SupplierDTO
                {
                    SupplierId = supplier.Id.ToString(),
                    Name = supplier.Name,
                    Category = supplier.Category,
                    ContactInfo = supplier.ContactInfo
                }
            };
        }
    }
}
