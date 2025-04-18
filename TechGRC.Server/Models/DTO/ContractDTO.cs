namespace TechGRC.Server.Models.DTO
{
    public class ContractDTO
    {
        public string ContractId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public SupplierDTO Supplier { get; set; }
    }
}
