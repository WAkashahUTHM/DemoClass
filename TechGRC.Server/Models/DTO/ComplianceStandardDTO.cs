namespace TechGRC.Server.Models.DTO
{
    public class ComplianceStandardDTO
    {
        public Guid ComplianceStandardId { get; set; } // Unique ID for compliance standard
        public string StandardName { get; set; }       // Name of the standard (e.g., "ISO 9001")
        public string Description { get; set; }        // Description of the compliance standard
        public DateTime LastChecked { get; set; }      // Date when the compliance was last checked
        public bool IsCompliant { get; set; }          // Boolean indicating if the process is compliant or not
    }
}
