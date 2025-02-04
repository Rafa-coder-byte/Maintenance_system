using maintenance_calibration_system.Domain.Datos_de_Configuracion;
using maintenance_calibration_system.Domain.Datos_Historicos;


namespace maintenance_calibration_system.Contacts
{
    public interface IMaintenanceActivityRepository<T> where T : MaintenanceActivity
    {
        /// <summary>Añade una nueva entidad al repositorio.</summary>
        /// <param name="maintenance">La entidad a añadir.</param>
        void Add(T maintenance);

        /// <summary>Busca una entidad por su identificador único.</summary>
        /// <param name="id">El identificador único de la entidad.</param>
        /// <returns>La entidad correspondiente al identificador, o null si no se encuentra.</returns>
        T GetById(Guid id);

        /// <summary>Devuelve todas las entidades del tipo especificado.</summary>
        /// <returns>Una colección de todas las entidades.</returns>
        IEnumerable<T> GetAll();

        /// <summary>Actualiza una entidad existente en el repositorio.</summary>
        /// <param name="maintenance">La entidad a actualizar.</param>
        void Update(T maintenance);

        /// <summary>Elimina una entidad del repositorio por su identificador único.</summary>
        /// <param name="id">El identificador único de la entidad a eliminar.</param>
        void Delete(Guid id);
    }
}
