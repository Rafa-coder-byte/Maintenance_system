using maintenance_calibration_system.Domain.Types;
using maintenance_calibration_system.Domain.Datos_de_Configuracion;

namespace maintenance_calibration_system.Domain.Datos_Historicos
{
    /// <summary>Representa una actividad de mantenimiento en el sistema de mantenimiento y calibración.</summary>
    public class Maintenance : MaintenanceActivity
    {
        #region Properties

        /// <summary>Tipo de mantenimiento.</summary>
        public TypeMaintenance TypeMaintenance { get; set; }

        /// <summary>Lista de actuadores en mantenimiento.</summary>
        public List<Actuador> MaintenanceActuador { get; set; }
        
        #endregion

        
        /// <summary>Constructor por defecto.</summary>
        public Maintenance() 
        {
            MaintenanceActuador = new List<Actuador>();
        }


        /// <summary>Constructor para crear una instancia de Maintenance.</summary>
        /// <param name="id">Identificador único de la actividad de mantenimiento.</param>
        /// <param name="dateActivity">Fecha de la actividad de mantenimiento.</param>
        /// <param name="typeMaintenance">Tipo de mantenimiento.</param>
        /// <param name="nameTechnician">Nombre del técnico que realizó el mantenimiento.</param>
        public Maintenance(Guid id, DateTime dateActivity, TypeMaintenance typeMaintenance, string nameTechnician)
            : base(id, dateActivity, nameTechnician)
        {
            TypeMaintenance = typeMaintenance;
            MaintenanceActuador = new List<Actuador>();
        }

        public Maintenance(Guid id, DateTime dateActivity, TypeMaintenance typeMaintenance, string nameTechnician, List<Actuador> maintenanceActuador)
           : base(id, dateActivity, nameTechnician)
        {
            TypeMaintenance = typeMaintenance;
            MaintenanceActuador = maintenanceActuador;
        }
    }
}
