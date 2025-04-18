using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;
using TechGRC.Server.Models;

namespace TechGRC.Server.Data
{
    public class TechGrcDbContext : DbContext
    {
        public TechGrcDbContext(DbContextOptions<TechGrcDbContext> options) : base(options) { }

        public DbSet<ProductionProcess> ProductionProcesses { get; set; }
        public DbSet<Machine> Machines { get; set; }
        public DbSet<MachineApplication> MachineApplications { get; set; }
        public DbSet<MaintenancePlan> MaintenancePlans { get; set; }
        public DbSet<QualityControl> QualityControls { get; set; }
        public DbSet<SupplyContract> SupplyContracts { get; set; }
        public DbSet<SupplierPartner> SupplierPartners { get; set; }
        public DbSet<RiskManagement> RiskManagement { get; set; }
        public DbSet<ComplianceStandard> ComplianceStandards { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Many-to-Many Relationship between ProductionProcess and MachineApplication
            modelBuilder.Entity<ProductionProcess>()
                .HasMany(p => p.MachineApplications)
                .WithMany(m => m.ProductionProcesses);

            // One-to-Many between SupplyContract and SupplierPartner
            modelBuilder.Entity<SupplyContract>()
                .HasOne(s => s.SupplierPartner)
                .WithMany(p => p.SupplyContracts)
                .HasForeignKey(s => s.SupplierPartnerId);

            // One-to-Many between RiskManagement and ProductionProcess
            modelBuilder.Entity<RiskManagement>()
                .HasOne(r => r.ProductionProcess)
                .WithMany(p => p.Risks)
                .HasForeignKey(r => r.ProductionProcessId);

            // Many-to-Many Relationship: ComplianceStandard ↔ ProductionProcess
            modelBuilder.Entity<ComplianceStandard>()
                .HasMany(c => c.LinkedProcesses)
                .WithMany(p => p.LinkedComplianceStandards);
        }
    }
}
