using maintenance_calibration_system.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace maintenance_calibration_system.DataAccess.FluentConfigurations.Common
{
    /// <summary>Clase abstracta base para la configuración de tipos de entidad.</summary>
    /// <typeparam name="T">Tipo de entidad que se está configurando.</typeparam>
    public abstract class EntityTypeConfigurationBase<T> : IEntityTypeConfiguration<T>
        where T : Entity
    {
        /// <summary>Configura el tipo de entidad especificado.</summary>
        /// <param name="builder">Constructor de la entidad.</param>
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasKey(x => x.Id); // Clave primaria
            builder.Property(x => x.Id).IsRequired(); // Propiedad Id es obligatoria
        }
    }
}

