using maintenance_calibration_system.Domain.Datos_Historicos;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using maintenance_calibration_system.Domain.Datos_de_Configuracion;



namespace maintenance_calibration_system.DataAccess.FluentConfigurations.MaintenanceActivities
{
    /// <summary>Configuración específica para la entidad Calibration.</summary>
    public class CalibrationEntityTypeConfiguration : IEntityTypeConfiguration<Calibration>
    {
        /// <summary>Configura la entidad Calibration.</summary>
        /// <param name="builder">Constructor de la entidad.</param>
        public void Configure(EntityTypeBuilder<Calibration> builder)
        {
            builder.ToTable("MaintenanceActivities"); // Mapea a la tabla "MaintenanceActivities"
            builder.HasBaseType(typeof(MaintenanceActivity)); // Hereda de la entidad base "MaintenanceActivity"

            // Configurando propiedades
            builder.Property(c => c.NameCertificateAuthority).IsRequired();

            // Configuración de la relación muchos a muchos con Sensor
            builder.HasMany(c => c.CalibratedSensors)
                .WithMany()
                .UsingEntity<Dictionary<string, object>>(
                    "SensorCalibration",
                    j => j.HasOne<Sensor>().WithMany().HasForeignKey("SensorId"),
                    j => j.HasOne<Calibration>().WithMany().HasForeignKey("CalibrationId"),
                    j => { j.HasKey("SensorId", "CalibrationId"); });
        }
    }
}

