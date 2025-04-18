namespace TechGRC.Server.Models
{
    public class ProductionProcess
    {
        public Guid Id { get; set; } // Primärschlüssel
        public string Name { get; set; } // Name des Prozesses
        public string Description { get; set; } // Beschreibung des Prozesses
        public DateTime StartDate { get; set; } // Beginn des Prozesses
        public DateTime EndDate { get; set; } // Ende des Prozesses (optional, für befristete Projekte)

        // Beziehung zu Maschinenanwendungen (m:n)
        public ICollection<MachineApplication> MachineApplications { get; set; }
        // Beziehung zu Risikomanagement
        public ICollection<RiskManagement> Risks { get; set; }
        // Beziehung zu Wartungsplänen
        public ICollection<MaintenancePlan> MaintenancePlans { get; set; }
        public ICollection<ComplianceStandard> LinkedComplianceStandards { get; set; } // Prozesse, die diesen Standard erfüllen müssen

    }
}
