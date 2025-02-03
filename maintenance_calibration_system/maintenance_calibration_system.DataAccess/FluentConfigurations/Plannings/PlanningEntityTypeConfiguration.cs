using maintenance_calibration_system.Domain.Datos_de_Planificación;
using maintenance_calibration_system.Domain.Datos_Historicos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;



namespace maintenance_calibration_system.DataAccess.FluentConfigurations.Plannings
{
    /// <summary>Configuración específica para la entidad Planning.</summary>
    public class PlanningEntityTypeConfiguration : IEntityTypeConfiguration<Planning>
    {
        /// <summary>Configura la entidad Planning.</summary>
        /// <param name="builder">Constructor de la entidad.</param>
        public void Configure(EntityTypeBuilder<Planning> builder)
        {
            builder.ToTable("Planes"); // Mapea a la tabla "Planes"
            builder.Property(x => x.EquipmentElement).IsRequired();
            builder.Property(x => x.ExecutionDate).IsRequired();

            // Relación uno a muchos con Calibration
            builder.HasMany<Calibration>()
                .WithOne()
                .HasForeignKey("PlanningId")
                .OnDelete(DeleteBehavior.Cascade); // En caso de eliminar una planificación, se eliminan las calibraciones asociadas a esta

            // Relación uno a muchos con Maintenance
            builder.HasMany<Maintenance>()
                .WithOne()
                .HasForeignKey("PlanningId")
                .OnDelete(DeleteBehavior.Cascade); // En caso de eliminar una planificación, se eliminan los mantenimientos asociados a esta
        }
    }
}


