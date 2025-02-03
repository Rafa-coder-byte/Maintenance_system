using maintenance_calibration_system.Domain.Datos_de_Planificación;
using maintenance_calibration_system.DataAccess.Contexts;
using Microsoft.EntityFrameworkCore;
using maintenance_calibration_system.Contracts;
using maintenance_calibration_system.Domain.Common;

namespace maintenance_calibration_system.DataAccess.Respositories.Plannings
{

    /// <summary>Repositorio para manejar entidades de tipo Planning.</summary>
    public class PlanningRepository : IPlanningRepository
    {
        private readonly ApplicationContext _context; // Contexto de la base de datos
        private readonly DbSet<Planning> _plannings; // Conjunto de planificaciones

        /// <summary>Constructor que inicializa el repositorio con el contexto de la aplicación.</summary>
        /// <param name="context">El contexto de la aplicación.</param>
        public PlanningRepository(ApplicationContext context)
        {
            _context = context;
            _plannings = _context.Set<Planning>(); // Inicializa el conjunto de planificaciones
        }

        /// <summary>Añade una planificación al conjunto y guarda los cambios.</summary>
        /// <param name="planning">La planificación a añadir.</param>
        public void Add(Planning planning)
        {
            if (planning == null)
            {
                throw new ArgumentNullException(nameof(planning), "La planificación no puede ser nula.");
            }
            _plannings.Add(planning);
            _context.SaveChanges();
        }

        /// <summary>Obtiene una planificación por su identificador.</summary>
        /// <param name="id">El identificador de la planificación.</param>
        /// <returns>La planificación encontrada o null.</returns>
        public Planning? GetById(Guid id)
        {
            return _plannings.FirstOrDefault(p => p.Id == id);
        }

        /// <summary>Obtiene todas las planificaciones.</summary>
        /// <returns>Una lista de todas las planificaciones.</returns>
        public IEnumerable<Planning> GetAll()
        {
            return _plannings.ToList();
        }

        /// <summary>Actualiza una planificación existente y guarda los cambios.</summary>
        /// <param name="planning">La planificación a actualizar.</param>
        public void Update(Planning planning)
        {
            // Verificar si la entidad ya está siendo rastreada
            var localEntity = _plannings.Local.FirstOrDefault(e => e.Id == planning.Id);
            if (localEntity != null)
            {
                // Si la entidad ya está siendo rastreada, desadjuntarla
                _context.Entry(localEntity).State = EntityState.Detached;
            }

            // Ahora puedes adjuntar la nueva instancia
            _context.Update(planning);
            _context.SaveChanges();

            var existingPlanning = GetById(planning.Id);
            if (existingPlanning != null)
            {
                existingPlanning.EquipmentElement = planning.EquipmentElement;
                existingPlanning.Type = planning.Type;
                existingPlanning.ExecutionDate = planning.ExecutionDate;
                _context.SaveChanges();
            }
        }

        /// <summary>Elimina una planificación por su identificador y guarda los cambios.</summary>
        /// <param name="id">El identificador de la planificación a eliminar.</param>
        public void Delete(Guid id)
        {
            var planning = GetById(id);
            if (planning != null)
            {
                _plannings.Remove(planning);
                _context.SaveChanges();
            }
        }
    }
}
