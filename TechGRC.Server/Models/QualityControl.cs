namespace TechGRC.Server.Models
{
    public class QualityControl
    {
        public Guid Id { get; set; } // Primärschlüssel
        public Guid ProductionProcessId { get; set; } // Fremdschlüssel zu ProductionProcess
        public ProductionProcess ProductionProcess { get; set; } // Beziehung zu Fertigungsprozess
        public DateTime ControlDate { get; set; } // Datum der Qualitätskontrolle
        public string Result { get; set; } // Ergebnis der Kontrolle (z. B. "Bestanden", "Fehlerhaft")
        public string Comments { get; set; } // Anmerkungen zur Kontrolle
    }
}
