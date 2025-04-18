namespace TechGRC.Server.Models
{
    public class ComplianceStandard
    {
        public Guid Id { get; set; } // Primärschlüssel
        public string StandardName { get; set; } // Name des Standards (z. b. "ISO 9001")
        public string Description { get; set; } // Beschreibung des Standards
        public ICollection<ProductionProcess> LinkedProcesses { get; set; } // Prozesse, die diesen Standard erfüllen müssen
        public DateTime LastChecked { get; set; }  // Datum der letzten Überprüfung
        public bool IsCompliant { get; set; }     // Compliance-Status (ob der Standard erfüllt ist)

    }
}
