using maintenance_calibration_system.DataAccess.FluentConfigurations.Common;
using maintenance_calibration_system.Domain.Datos_de_Configuracion;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace maintenance_calibration_system.DataAccess.FluentConfigurations.Equipments
{
    /// <summary>Configuración base para la entidad Equipment.</summary>
    public class EquipmentEntityTypeConfigurationBase : EntityTypeConfigurationBase<Equipment>
    {
        /// <summary>Configura la entidad Equipment.</summary>
        /// <param name="builder">Constructor de la entidad.</param>
        public override void Configure(EntityTypeBuilder<Equipment> builder)
        {
            // Configurando tabla para los equipamientos
            builder.ToTable("Equipments");
            base.Configure(builder);

            // Configurando propiedades
            builder.OwnsOne(e => e.Magnitude, mg =>
            {
                mg.Property(p => p.Name).IsRequired();
                mg.Property(p => p.UnitofMagnitude).IsRequired();
            });

            builder.Property(e => e.Manufacturer).IsRequired();
            builder.Property(e => e.AlphanumericCode).IsRequired();

            // Configuración de la discriminación de herencia
            builder.HasDiscriminator<string>("EquipmentType")
                .HasValue<Sensor>("Sensor")
                .HasValue<Actuador>("Actuador");
        }
    }
}



