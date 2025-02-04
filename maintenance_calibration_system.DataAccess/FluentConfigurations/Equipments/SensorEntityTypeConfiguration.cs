using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using maintenance_calibration_system.Domain.Datos_de_Configuracion;


namespace maintenance_calibration_system.DataAccess.FluentConfigurations.Equipments
{
    /// <summary>Configuración específica para la entidad Sensor.</summary>
    public class SensorEntityTypeConfiguration : IEntityTypeConfiguration<Sensor>
    {
        /// <summary>Configura la entidad Sensor.</summary>
        /// <param name="builder">Constructor de la entidad.</param>
        public void Configure(EntityTypeBuilder<Sensor> builder)
        {
            builder.ToTable("Equipments"); // Mapea a la tabla "Equipments"
            builder.HasBaseType(typeof(Equipment)); // Hereda de la entidad base "Equipment"

            // Configurando propiedades
            builder.Property(s => s.Protocol).IsRequired();
            builder.Property(s => s.PrincipleOperation);
            builder.Property(s => s.Calibrated).IsRequired();
        }
    }
}

