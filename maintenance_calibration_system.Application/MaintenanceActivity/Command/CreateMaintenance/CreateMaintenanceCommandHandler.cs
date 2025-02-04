using maintenance_calibration_system.Application.Abstract;
using maintenance_calibration_system.Application.MaintenanceActivity.Command.CreateMaintenance;
using maintenance_calibration_system.Domain.Datos_Historicos;
using maintenance_calibration_system.Domain.Datos_de_Configuracion;
using maintenance_calibration_system.Domain.Types;
using maintenance_calibration_system.Contacts;

namespace maintenance_calibration_system.Application.MaintenanceActivity.CommandHandlers
{
    public class CreateMaintenanceCommandHandler(
        IMaintenanceActivityRepository<Maintenance> maintenanceRepository,
         IUnitOfWork unitOfWork)
                 : ICommandHandler<CreateMaintenanceCommand, Maintenance>
    {
        private readonly IMaintenanceActivityRepository<Maintenance> _maintenanceRepository = (IMaintenanceActivityRepository<Maintenance>)maintenanceRepository; // Cambiado para usar el repositorio de calibraciones
        private readonly IUnitOfWork _unitOfWork = unitOfWork;


        public Task<Maintenance> Handle(CreateMaintenanceCommand request, CancellationToken cancellationToken)
        {
            // Crear una nueva actividad de mantenimiento
            var result = new Maintenance(
                Guid.NewGuid(),
                request.DateActivity,
                request.typeMaintenance,
                request.NameTechnician);

            _maintenanceRepository.Add(result);
            _unitOfWork.SaveChanges();

            return Task.FromResult(result);
        }
    }
}