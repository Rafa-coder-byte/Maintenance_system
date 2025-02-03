using maintenance_calibration_system.Contacts;
using maintenance_calibration_system.DataAccess.Contexts;
using maintenance_calibration_system.Domain.Datos_de_Configuracion; // Para ApplicationContext


namespace maintenance_calibration_system.DataAccess.Respositories.Equipments
{
    /// <summary>Repositorio para manejar entidades de tipo Equipment.</summary>
    /// <remarks>Constructor que inicializa el repositorio con el contexto de la aplicación.</remarks>
    /// <param name="context">El contexto de la aplicación.</param>
    public class EquipmentRepository<T>(ApplicationContext context) : RepositoryBase<T>(context), IEquipmentRepository<T> where T : Equipment
    {
    }
}
    

