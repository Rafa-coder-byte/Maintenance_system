using maintenance_calibration_system.Domain.Common;
using maintenance_calibration_system.Domain.Types;
using System.Xml.Linq;

namespace maintenance_calibration_system.Domain.Datos_de_Planificación
{
    /// <summary>Representa un evento de planificación para futuras calibraciones o mantenimiento.</summary>
    public class Planning : Entity
    {
        #region Properties
        /// <summary>Equipo que planificó el evento.</summary>
        public string EquipmentElement { get; set; }

        /// <summary>Tipo de planificación.</summary>
        public PlanningTypes Type { get; set; }

        /// <summary>Fecha de ejecución.</summary>
        public DateTime ExecutionDate { get; set; }
        #endregion


        /// <summary>Constructor por defecto.</summary>
        public Planning() 
        {
            EquipmentElement = "Default";
        }

        /// <summary>Constructor para crear una instancia de Planning.</summary>
        /// <param name="id">Identificador único del evento de planificación.</param>
        /// <param name="equipmentElement">Equipo que planificó el evento.</param>
        /// <param name="type">Tipo de planificación.</param>
        /// <param name="executionDate">Fecha de ejecución.</param>
        public Planning(Guid id, string equipmentElement, PlanningTypes type, DateTime executionDate)
            : base(id)
        {
            EquipmentElement = equipmentElement ?? throw new ArgumentNullException(nameof(equipmentElement)); // Lanza excepción si EquipmentElement es nulo;;
            Type = type;
            ExecutionDate = executionDate;
        }
    }
}

