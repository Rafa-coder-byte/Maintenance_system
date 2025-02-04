using maintenance_calibration_system.Application.Abstract;
using maintenance_calibration_system.Application.MaintenanceActivity.Command.CreateCalibration;
using maintenance_calibration_system.Contacts;
using maintenance_calibration_system.Domain.Datos_Historicos; // Asegúrate de que este espacio de nombres sea correcto


namespace maintenance_calibration_system.Application.MaintenanceActivity.Queries.GetCalibration
{
    public class GetCalibrationByIdQueryHandler(IMaintenanceActivityRepository<Calibration> calibrationRepository) : IQueryHandler<GetCalibrationByIdQuery, Calibration>
    {
        private readonly IMaintenanceActivityRepository<Calibration> _calibrationRepository = calibrationRepository; // Repositorio para manejar calibraciones

        public Task<Calibration> Handle(GetCalibrationByIdQuery request, CancellationToken cancellationToken)
        {
            // Obtener la calibración del repositorio por ID
            Calibration calibration = (Calibration)_calibrationRepository.GetById(request.Id);

            return Task.FromResult(calibration); // Retornar la calibración encontrada
        }
    }
}
