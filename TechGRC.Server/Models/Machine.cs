namespace TechGRC.Server.Models
{
    public class Machine
    {
        public Guid Id { get; set; } // Primärschlüssel
        public string Name { get; set; } // Name der Maschine (z. B. CNC-Fräsmaschine)
        public string Type { get; set; } // Typ der Maschine (z. B. Werkzeugmaschine)
        public string Manufacturer { get; set; } // Hersteller der Maschine
        public string Model { get; set; } // Modellnummer

        // Beziehung zu Maschinenanwendungen (1:n)
        public ICollection<MachineApplication> Applications { get; set; }
        // Beziehung zu Wartungsplänen
        public ICollection<MaintenancePlan> MaintenancePlans { get; set; }
    }

}
