using maintenance_calibration_system.Domain.Datos_de_Planificación;


namespace maintenance_calibration_system.Contracts
{
    /// <summary>Interfaz para el repositorio de planificaciones.</summary>
    public interface IPlanningRepository
    {
        /// <summary>Añade una planificación.</summary>
        void Add(Planning planning);

        /// <summary>Obtiene una planificación por su identificador.</summary>
        Planning? GetById(Guid id);

        /// <summary>Obtiene todas las planificaciones.</summary>
        IEnumerable<Planning> GetAll();

        /// <summary>Actualiza una planificación existente.</summary>
        void Update(Planning planning);

        /// <summary>Elimina una planificación por su identificador.</summary>
        void Delete(Guid id);
    }
}
