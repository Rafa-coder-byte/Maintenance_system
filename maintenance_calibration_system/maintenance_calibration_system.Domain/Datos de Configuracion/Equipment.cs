using maintenance_calibration_system.Domain.Common;
using maintenance_calibration_system.Domain.ValueObjects;
using System.Xml.Linq;


namespace maintenance_calibration_system.Domain.Datos_de_Configuracion
{
    /// <summary>Clase abstracta que representa un equipo en el sistema de mantenimiento y calibración.</summary>
    public abstract class Equipment : Entity
    {
        #region Properties

        /// <summary>Código alfanumérico del equipo.</summary>
        public string AlphanumericCode { get; set; }

        /// <summary>Magnitud física asociada.</summary>
        public PhysicalMagnitude Magnitude { get; set; }

        /// <summary>Nombre del fabricante.</summary>
        public string Manufacturer { get; set; }

        #endregion


        /// <summary>Constructor por defecto.</summary>
        public Equipment()
        {
            Magnitude = new PhysicalMagnitude("Default Name", "Default UnitOfMagnitude");
            Manufacturer = "UnknownManufacturer";
            AlphanumericCode = "Default";
        }

        /// <summary>Constructor para crear una instancia de Equipment.</summary>
        /// <param name="id">Identificador único del equipo.</param>
        /// <param name="alphanumericCode">Código alfanumérico del equipo.</param>
        /// <param name="magnitude">Magnitud física asociada.</param>
        /// <param name="manufacturer">Nombre del fabricante.</param>
        protected Equipment(Guid id, string alphanumericCode, PhysicalMagnitude magnitude, string manufacturer="UnknownManufacturer")
            : base(id)
        {
            AlphanumericCode = alphanumericCode ?? throw new ArgumentNullException(nameof(AlphanumericCode)); // Lanza excepción si AlphanumericCode es nulo;;
            Magnitude = magnitude;
            Manufacturer = manufacturer;
        }
    }
}

