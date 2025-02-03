using maintenance_calibration_system.Application.Abstract;
using maintenance_calibration_system.Application.MaintenanceActivity.Command.CreateMaintenance;
using maintenance_calibration_system.Contacts;
using maintenance_calibration_system.Domain.Datos_Historicos; // Asegúrate de que este espacio de nombres sea correcto


namespace maintenance_calibration_system.Application.MaintenanceActivity.Command.DeleteMaintenance
{
    public class DeleteMaintenanceCommandHandler(
       IMaintenanceActivityRepository<Maintenance> maintenanceRepository,
        IUnitOfWork unitOfWork) : ICommandHandler<DeleteMaintenanceCommand, bool>
    {
        private readonly IMaintenanceActivityRepository<Maintenance> _maintenanceRepository = (IMaintenanceActivityRepository<Maintenance>)maintenanceRepository; // Repositorio para manejar calibraciones
        private readonly IUnitOfWork _unitOfWork = unitOfWork; // Unidad de trabajo para manejar transacciones

        public Task<bool> Handle(DeleteMaintenanceCommand request, CancellationToken cancellationToken)
        {
            bool result = true;

            try
            {
                _maintenanceRepository.Delete(request.Id); // Eliminar la calibración por ID
                _unitOfWork.SaveChanges(); // Guardar cambios en la unidad de trabajo
            }
            catch (Exception)
            {
                // Manejo de excepciones (opcional)
                result = false; // Si ocurre un error, se establece result a false
                // Aquí puedes registrar el error o manejarlo según sea necesario
            }

            return Task.FromResult(result); // Retornar el resultado de la operación
        }


    }
}