using maintenance_calibration_system.Application.Abstract;
using maintenance_calibration_system.Application.MaintenanceActivity.Command.CreateMaintenance;
using maintenance_calibration_system.Contacts;
using maintenance_calibration_system.Domain.Datos_Historicos;// Asegúrate de que este espacio de nombres sea correct
using maintenance_calibration_system.Application.MaintenanceActivity.Queries.GetAllCalibration;
using MediatR;
using System.Linq;
namespace maintenance_calibration_system.Application.MaintenanceActivity.Queries.GetAllMaintenance
{

    public class GetAllMaintenanceQueryHandler : IQueryHandler<GetAllMaintenanceQuery, List<Maintenance>>
    {
        private readonly IMaintenanceActivityRepository<Maintenance> _maintenanceRepository; // Repositorio para manejar mantenimientos

        // Constructor que inyecta el repositorio
        public GetAllMaintenanceQueryHandler(IMaintenanceActivityRepository<Maintenance> maintenanceRepository)
        {
            _maintenanceRepository = maintenanceRepository; // Asignar el repositorio de mantenimientos
        }

        public Task<List<Maintenance>> Handle(GetAllMaintenanceQuery request, CancellationToken cancellationToken)
        {
            // Obtener todas los mantenimientos del repositorio
            List<Maintenance> MaintenanceActuadores = _maintenanceRepository.GetAll().ToList();

            return Task.FromResult(MaintenanceActuadores); // Retornar la lista de calibraciones
        }

    }
}
