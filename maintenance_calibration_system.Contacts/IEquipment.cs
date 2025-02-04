using maintenance_calibration_system.Domain.Datos_de_Configuracion;

namespace maintenance_calibration_system.Contacts
{
    public interface IEquipmentRepository<T> where T : Equipment
    {
        /// <summary>Añade una nueva entidad al repositorio.</summary>
        /// <param name="equipment">La entidad a añadir.</param>
        void Add(T equipment);

        /// <summary>Busca una entidad por su identificador único.</summary>
        /// <param name="id">El identificador único de la entidad.</param>
        /// <returns>La entidad correspondiente al identificador, o null si no se encuentra.</returns>
        T? GetById(Guid id);

        /// <summary>Devuelve todas las entidades del tipo especificado.</summary>
        /// <returns>Una colección de todas las entidades.</returns>
        IEnumerable<T> GetAll();

        /// <summary>Actualiza una entidad existente en el repositorio.</summary>
        /// <param name="equipment">La entidad a actualizar.</param>
        void Update(T equipment);

        /// <summary>Elimina una entidad del repositorio por su identificador único.</summary>
        /// <param name="id">El identificador único de la entidad a eliminar.</param>
        void Delete(Guid id);
    }
}
