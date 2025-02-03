using maintenance_calibration_system.Domain.Datos_Historicos;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using maintenance_calibration_system.Domain.Datos_de_Configuracion;

namespace maintenance_calibration_system.DataAccess.FluentConfigurations.MaintenanceActivities
{
    /// <summary>Configuración específica para la entidad Maintenance.</summary>
    public class MaintenanceEntityTypeConfiguration : IEntityTypeConfiguration<Maintenance>
    {
        /// <summary>Configura la entidad Maintenance.</summary>
        /// <param name="builder">Constructor de la entidad.</param>
        public void Configure(EntityTypeBuilder<Maintenance> builder)
        {
            builder.ToTable("MaintenanceActivities"); // Mapea a la tabla "MaintenanceActivities"
            builder.HasBaseType(typeof(MaintenanceActivity)); // Hereda de la entidad base "MaintenanceActivity"

            // Configurando propiedades
            builder.Property(m => m.TypeMaintenance).IsRequired();

            // Configuración de la relación muchos a muchos con Actuator
            builder.HasMany(m => m.MaintenanceActuador)
                .WithMany()
                .UsingEntity<Dictionary<string, object>>(
                    "MaintenanceActuator",
                    j => j.HasOne<Actuador>().WithMany().HasForeignKey("ActuatorId"),
                    j => j.HasOne<Maintenance>().WithMany().HasForeignKey("MaintenanceId"),
                    j => { j.HasKey("MaintenanceId", "ActuatorId"); });
        }
    }
}
