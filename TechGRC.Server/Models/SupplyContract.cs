namespace TechGRC.Server.Models
{
    public class SupplyContract
    {
        public Guid Id { get; set; } // Primärschlüssel
        public string ContractTitle { get; set; } // Titel des Vertrages
        public string Description { get; set; } // Vertragsbeschreibung
        public DateTime StartDate { get; set; } // Vertragsbeginn
        public DateTime EndDate { get; set; } // Vertragsende
        public Guid SupplierPartnerId { get; set; } // Fremdschlüssel zu SupplierPartner
        public SupplierPartner SupplierPartner { get; set; } // Beziehung zu Zuliefererpartner
    }
}
