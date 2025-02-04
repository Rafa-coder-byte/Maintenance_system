using maintenance_calibration_system.Application.Abstract;
using maintenance_calibration_system.Application.MaintenanceActivity.Command.CreateCalibration;
using maintenance_calibration_system.Contacts;
using maintenance_calibration_system.Domain.Datos_Historicos; // Asegúrate de que este espacio de nombres sea correct
using MediatR;
using System.Linq;
namespace maintenance_calibration_system.Application.MaintenanceActivity.Queries.GetAllCalibration
{

    public class GetAllCalibrationQueryHandler : IQueryHandler<GetAllCalibrationQuery, List<Calibration>>
    {
        private readonly IMaintenanceActivityRepository<Calibration> _calibrationRepository; // Repositorio para manejar calibraciones

        // Constructor que inyecta el repositorio
        public GetAllCalibrationQueryHandler(IMaintenanceActivityRepository<Calibration> calibrationRepository)
        {
            _calibrationRepository = calibrationRepository; // Asignar el repositorio de calibraciones
        }

        public Task<List<Calibration>> Handle(GetAllCalibrationQuery request, CancellationToken cancellationToken)
        {
            // Obtener todas las calibraciones del repositorio
            List<Calibration> CalibratedSensors = _calibrationRepository.GetAll().ToList();

            return Task.FromResult(CalibratedSensors); // Retornar la lista de calibraciones
        }

    }  
}