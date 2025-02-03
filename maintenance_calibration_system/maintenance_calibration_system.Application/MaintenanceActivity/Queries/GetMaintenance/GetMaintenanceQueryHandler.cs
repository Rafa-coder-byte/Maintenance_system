using maintenance_calibration_system.Application.Abstract;
using maintenance_calibration_system.Application.MaintenanceActivity.Command.CreateMaintenance;
using maintenance_calibration_system.Application.MaintenanceActivity.Queries.GetCalibration;
using maintenance_calibration_system.Contacts;
using maintenance_calibration_system.Domain.Datos_Historicos; // Asegúrate de que este espacio de nombres sea correcto


namespace maintenance_calibration_system.Application.MaintenanceActivity.Queries.GetMaintenance
{
    public class GetMaintenanceByIdQueryHandler(IMaintenanceActivityRepository<Maintenance> maintenanceRepository) : IQueryHandler<GetMaintenanceByIdQuery, Maintenance>
    {
        private readonly IMaintenanceActivityRepository<Maintenance> _maintenanceRepository = maintenanceRepository; // Repositorio para manejar calibraciones

        public Task<Maintenance> Handle(GetMaintenanceByIdQuery request, CancellationToken cancellationToken)
        {
            // Obtener el mantenimiento del repositorio por ID
            Maintenance maintenance = (Maintenance)_maintenanceRepository.GetById(request.Id);

            return Task.FromResult(maintenance); // Retornar el mantenimiento encontrada
        }
    }
}