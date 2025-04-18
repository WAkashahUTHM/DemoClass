namespace TechGRC.Server.Models
{
    public class MaintenancePlan
    {
        public Guid Id { get; set; } // Primärschlüssel
        public Guid MachineId { get; set; } // Fremdschlüssel zu Machine
        public Machine Machine { get; set; } // Beziehung zu Maschine
        public DateTime ScheduledDate { get; set; } // Geplantes Wartungsdatum
        public string TaskDescription { get; set; } // Beschreibung der Wartungsaufgabe
        public string Status { get; set; } // Status der Wartung (z. B. "Geplant", "Abgeschlossen")

        // Beziehung zu Fertigungsprozessen
        public ICollection<ProductionProcess> LinkedProcesses { get; set; }
    }
}
