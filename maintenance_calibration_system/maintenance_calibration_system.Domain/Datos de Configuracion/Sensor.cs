using maintenance_calibration_system.Domain.Types;
using maintenance_calibration_system.Domain.ValueObjects;

namespace maintenance_calibration_system.Domain.Datos_de_Configuracion
{
    /// <summary>Representa un sensor en el sistema de mantenimiento y calibración.</summary>
    public class Sensor : Equipment
    {
        #region Properties
        /// <summary>Principio de operación del sensor.</summary>
        public string? PrincipleOperation { get; set; }

        /// <summary>Indica si el sensor está calibrado (predeterminado: false).</summary>
        public bool Calibrated { get; set; } = false;

        /// <summary>Protocolo de comunicación utilizado por el sensor.</summary>
        public CommunicationProtocol Protocol { get; set; }
        #endregion

        /// <summary>Constructor necesario para EntityFramework y la creación de la base de datos.</summary>
        public Sensor() { }

        /// <summary>Constructor para crear una instancia de Sensor.</summary>
        /// <param name="id">Identificador único del sensor.</param>
        /// <param name="alphanumericCode">Código alfanumérico del sensor.</param>
        /// <param name="magnitude">Magnitud física medida por el sensor.</param>
        /// <param name="manufacturer">Fabricante del sensor.</param>
        /// <param name="protocol">Protocolo de comunicación utilizado por el sensor.</param>
        /// <param name="principleOperation">Principio de operación del sensor.</param>


        public Sensor(Guid id, string alphanumericCode, PhysicalMagnitude magnitude, string manufacturer, CommunicationProtocol protocol, string? principleOperation)
            : base(id, alphanumericCode, magnitude, manufacturer)
        {
            Protocol = protocol;
            PrincipleOperation = principleOperation;
        }
    }
}
