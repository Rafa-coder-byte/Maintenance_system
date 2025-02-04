using maintenance_calibration_system.Domain.Common;

namespace maintenance_calibration_system.Domain.Datos_Historicos
{
    /// <summary>Clase abstracta que representa una actividad de mantenimiento en el sistema de mantenimiento y calibración.</summary>
    public abstract class MaintenanceActivity : Entity
    {
        #region Properties

        /// <summary>Fecha en que se realizó la actividad.</summary>
        public DateTime DateActivity { get; set; }

        /// <summary>Nombre del técnico que realizó la actividad.</summary>
        public string NameTechnician { get; set; }
        
        #endregion

        /// <summary>Constructor por defecto.</summary>
        public MaintenanceActivity()
        {
            NameTechnician = "UnknownTechnician";
        }

        /// <summary>Constructor para crear una instancia de MaintenanceActivity.</summary>
        /// <param name="id">Identificador único de la actividad de mantenimiento.</param>
        /// <param name="dateActivity">Fecha de la actividad de mantenimiento.</param>
        /// <param name="nameTechnician">Nombre del técnico que realizó la actividad.</param>
        protected MaintenanceActivity(Guid id, DateTime dateActivity, string nameTechnician= "UnknownTechnician")
            : base(id)
        {
            DateActivity = dateActivity;
            NameTechnician = nameTechnician;
        }
    }
}

