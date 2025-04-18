namespace TechGRC.Server.Models.DTO
{
    public class CreateContractDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Guid SupplierId { get; set; }
    }
}
