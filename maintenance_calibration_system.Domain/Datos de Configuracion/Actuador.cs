using maintenance_calibration_system.Domain.Types;
using maintenance_calibration_system.Domain.ValueObjects;

namespace maintenance_calibration_system.Domain.Datos_de_Configuracion
{
    /// <summary>Representa un actuador en el sistema de mantenimiento y calibración.</summary>
    public class Actuador : Equipment
    {
        #region Properties
        /// <summary>Código de control.</summary>
        public string? CodeControl { get; set; }

        /// <summary>Control de señal.</summary>
        public SignalControl SignalControl { get; set; }

        /// <summary>Indica si requiere mantenimiento (predeterminado: false).</summary>
        public bool Maintenance { get; set; } = false;
        #endregion

        /// <summary>Constructor necesario para EntityFramework y la creación de la base de datos.</summary>
        public Actuador() { }

        /// <summary>Constructor para crear una instancia de Actuador.</summary>
        /// <param name="id">Identificador único del actuador.</param>
        /// <param name="alphanumericCode">Código alfanumérico del actuador.</param>
        /// <param name="magnitude">Magnitud física medida por el actuador.</param>
        /// <param name="manufacturer">Fabricante del actuador.</param>
        /// <param name="codeControl">Código de control del actuador.</param>
        /// <param name="signalControl">Tipo de control de señal del actuador.</param>
        public Actuador(Guid id, string alphanumericCode, PhysicalMagnitude magnitude, string manufacturer, string? codeControl, SignalControl signalControl)
            : base(id, alphanumericCode, magnitude, manufacturer)
        {
            CodeControl = codeControl;
            SignalControl = signalControl;
        }
    }
}
