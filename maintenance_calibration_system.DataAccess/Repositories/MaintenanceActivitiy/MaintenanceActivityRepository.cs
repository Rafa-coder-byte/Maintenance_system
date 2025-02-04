using maintenance_calibration_system.Domain.Datos_Historicos; 
using maintenance_calibration_system.DataAccess.Contexts;
using maintenance_calibration_system.Domain.Datos_de_Configuracion;
using maintenance_calibration_system.Contacts;
using maintenance_calibration_system.Domain.Common;
using Microsoft.EntityFrameworkCore;
//using System.Data.Entity;


namespace maintenance_calibration_system.DataAccess.Respositories.MaintenanceActivitiy
{

    /// <summary>Repositorio para manejar entidades de tipo MaintenanceActivity.</summary>
    /// <remarks>Constructor que inicializa el repositorio con el contexto de la aplicación.</remarks>
    /// <param name="context">El contexto de la aplicación.</param>
    public class MaintenanceActivityRepository<T> : RepositoryBase<T>, IMaintenanceActivityRepository<T> where T : MaintenanceActivity
    {
        public MaintenanceActivityRepository(ApplicationContext context) : base(context)
        {
        }

        // Sobrescribiendo el método GetById para incluir la carga anticipada de sensores y actuadores
        public override T GetById(Guid id)
        {
            var query = _context.Set<T>().AsQueryable();

            if (typeof(T) == typeof(Calibration))
            {
                query = query.Include(ma => ((Calibration)(object)ma).CalibratedSensors);
            }
            else if (typeof(T) == typeof(Maintenance))
            {
                query = query.Include(ma => ((Maintenance)(object)ma).MaintenanceActuador); // Incluir actuadores de mantenimiento
            }

            return query.FirstOrDefault(ma => ma.Id == id);
        }

        // Sobrescribiendo el método Update para manejar las relaciones específicas
        public override void Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity), "La entidad no puede ser nula.");
            }

            try
            {
                var localEntity = _context.Set<T>().Local.FirstOrDefault(e => e.Id == entity.Id);
                if (localEntity != null)
                {
                    _context.Entry(localEntity).State = EntityState.Detached;
                }

                if (entity is Calibration calibration)
                {
                    // Manejar la relación entre Calibration y Sensors
                    var existingCalibration = _context.Set<Calibration>()
                        .Include(c => c.CalibratedSensors)
                        .FirstOrDefault(c => c.Id == calibration.Id);

                    if (existingCalibration != null)
                    {
                        // Remover relaciones existentes de sensores
                        existingCalibration.CalibratedSensors.Clear();
                        _context.SaveChanges();

                        // Adjuntar sensores y actualizar relaciones
                        foreach (var sensor in calibration.CalibratedSensors)
                        {
                            var localSensor = _context.Set<Sensor>().Local.FirstOrDefault(s => s.Id == sensor.Id);
                            if (localSensor != null)
                            {
                                _context.Entry(localSensor).State = EntityState.Detached;
                            }
                            _context.Attach(sensor);

                            if (!existingCalibration.CalibratedSensors.Contains(sensor))
                            {
                                existingCalibration.CalibratedSensors.Add(sensor);
                            }
                        }

                        _context.Entry(existingCalibration).State = EntityState.Modified;
                    }
                }
                else if (entity is Maintenance maintenance)
                {
                    // Manejar la relación entre Maintenance y Actuators
                    var existingMaintenance = _context.Set<Maintenance>()
                        .Include(m => m.MaintenanceActuador)
                        .FirstOrDefault(m => m.Id == maintenance.Id);

                    if (existingMaintenance != null)
                    {
                        // Remover relaciones existentes de actuadores
                        existingMaintenance.MaintenanceActuador.Clear();
                        _context.SaveChanges();

                        // Adjuntar actuadores y actualizar relaciones
                        foreach (var actuador in maintenance.MaintenanceActuador)
                        {
                            var localActuador = _context.Set<Actuador>().Local.FirstOrDefault(a => a.Id == actuador.Id);
                            if (localActuador != null)
                            {
                                _context.Entry(localActuador).State = EntityState.Detached;
                            }
                            _context.Attach(actuador);

                            if (!existingMaintenance.MaintenanceActuador.Contains(actuador))
                            {
                                existingMaintenance.MaintenanceActuador.Add(actuador);
                            }
                        }

                        _context.Entry(existingMaintenance).State = EntityState.Modified;
                    }
                }

                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al actualizar la entidad: {ex.Message}");
                throw;
            }
        }


        // Implementando el método GetAll para incluir sensores o actuadores respectivamente
        public override IEnumerable<T> GetAll()
        {
            var query = _context.Set<T>().AsQueryable();

            if (typeof(T) == typeof(Calibration))
            {
                query = query.Include(ma => ((Calibration)(object)ma).CalibratedSensors);
            }
            else if (typeof(T) == typeof(Maintenance))
            {
                query = query.Include(ma => ((Maintenance)(object)ma).MaintenanceActuador);
            }

            return query.ToList();
        }

    }
}
