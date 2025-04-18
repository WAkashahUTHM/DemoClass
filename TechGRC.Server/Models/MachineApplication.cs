namespace TechGRC.Server.Models
{
    public class MachineApplication
    {
        public Guid Id { get; set; } // Primärschlüssel
        public string Name { get; set; } // Name der Anwendung (z. B. "CNC Steuerung")
        public string Description { get; set; } // Beschreibung der Anwendung
        public string Type { get; set; } // Typ der Anwendung (z. B. Automatisierung)

        // Beziehung zu Fertigungsprozessen (m:n)
        public ICollection<ProductionProcess> ProductionProcesses { get; set; }
        // Beziehung zu Maschinen (1:n)
        public ICollection<Machine> Machines { get; set; }
    }
}
