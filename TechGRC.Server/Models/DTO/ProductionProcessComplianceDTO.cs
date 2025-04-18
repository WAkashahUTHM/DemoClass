namespace TechGRC.Server.Models.DTO
{
    public class ProductionProcessComplianceDTO
    {
        public Guid ProductionProcessId { get; set; }  // Unique ID for the production process
        public string ProcessName { get; set; }         // Name of the process (e.g., "CNC Machine Assembly")
        public ICollection<ComplianceStandardDTO> LinkedComplianceStandards { get; set; }  // List of linked compliance standards
    }
}
