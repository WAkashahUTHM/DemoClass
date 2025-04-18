namespace TechGRC.Server.Models
{
    public class RiskManagement
    {
        public Guid Id { get; set; } // Primärschlüssel
        public Guid ProductionProcessId { get; set; } // Fremdschlüssel zu ProductionProcess
        public ProductionProcess ProductionProcess { get; set; } // Beziehung zu Fertigungsprozess
        public string RiskDescription { get; set; } // Beschreibung des Risikos
        public int RiskLevel { get; set; } // Risikostufe (z. B. 1=gering, 5=hoch)
        public DateTime RiskAssessmentDate { get; set; } // Datum der Risikobewertung
    }
}
