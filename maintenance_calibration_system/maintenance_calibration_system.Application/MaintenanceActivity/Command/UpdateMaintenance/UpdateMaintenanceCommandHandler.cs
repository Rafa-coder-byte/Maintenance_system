using maintenance_calibration_system.Application.Abstract;
using maintenance_calibration_system.Contacts;
using maintenance_calibration_system.Domain.Datos_Historicos;// Asegúrate de que este espacio de nombres sea correcto
using maintenance_calibration_system.Application.Equipments.Commands.UpdateActuador;
using maintenance_calibration_system.Application.MaintenanceActivity.Command.CreateMaintenance;
using maintenance_calibration_system.Application.MaintenanceActivity.Command.CreateCalibration;

namespace maintenance_calibration_system.Application.MaintenanceActivity.Command.UpdateMaintenance
{
    public class UpdateMaintenanceCommandHandler(
       IMaintenanceActivityRepository<Maintenance> maintenanceRepository,
        IUnitOfWork unitOfWork) : ICommandHandler<UpdateMaintenanceCommand,bool>
    {
        private readonly IMaintenanceActivityRepository<Maintenance> _maintenanceRepository = (IMaintenanceActivityRepository<Maintenance>)maintenanceRepository; // Repositorio para manejar mantenimientos
        private readonly IUnitOfWork _unitOfWork = unitOfWork; // Unidad de trabajo para manejar transacciones

        public Task<bool> Handle(UpdateMaintenanceCommand request, CancellationToken cancellationToken)
        {
            // Buscar el mantenimiento existente
            var existingMaintenance = _maintenanceRepository.GetById(request.id);

            if (existingMaintenance == null)
            {
                return Task.FromResult(false); // Retorna false si el mantenimiento no existe
            }

            // Crear un nuevo objeto Maintenance con los valores actualizados usando el constructor
            var updatedMaintenance = new Maintenance(
                existingMaintenance.Id, // Mantener el mismo ID
                request.DateActivity,
                request.TypeMaintenance,
                request.NameTechnician);


            // Actualizar el mantenimiento en el repositorio
            _maintenanceRepository.Update(updatedMaintenance);
            _unitOfWork.SaveChanges();

            return Task.FromResult(true); // Devuelve true si la actualización fue exitosa
        }
    }
}
