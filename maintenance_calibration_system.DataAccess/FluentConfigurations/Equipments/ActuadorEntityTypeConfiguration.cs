using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using maintenance_calibration_system.Domain.Datos_de_Configuracion;


namespace maintenance_calibration_system.DataAccess.FluentConfigurations.Equipments
{
    /// <summary>Configuración específica para la entidad Actuador.</summary>
    public class ActuadorEntityTypeConfiguration : IEntityTypeConfiguration<Actuador>
    {
        /// <summary>Configura la entidad Actuador.</summary>
        /// <param name="builder">Constructor de la entidad.</param>
        public void Configure(EntityTypeBuilder<Actuador> builder)
        {
            builder.ToTable("Equipments"); // Mapea a la tabla "Equipments"
            builder.HasBaseType(typeof(Equipment)); // Hereda de la entidad base "Equipment"

            // Configurando propiedades
            builder.Property(a => a.SignalControl).IsRequired();
            builder.Property(a => a.CodeControl);
            builder.Property(a => a.Maintenance).IsRequired();
        }
    }
}



