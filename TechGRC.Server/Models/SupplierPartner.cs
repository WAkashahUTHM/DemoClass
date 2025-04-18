namespace TechGRC.Server.Models
{
    public class SupplierPartner
    {
        public Guid Id { get; set; } // Primärschlüssel
        public string Name { get; set; } // Name des Partners (z. B. "Maschinenlieferant XY")
        public string Category { get; set; } // Kategorie des Partners (z. b. "Zulieferer", "Dienstleister")
        public string ContactInfo { get; set; } // Kontaktinformationen des Partners
        public ICollection<SupplyContract> SupplyContracts { get; set; } // Verträge des Partners
    }
}
