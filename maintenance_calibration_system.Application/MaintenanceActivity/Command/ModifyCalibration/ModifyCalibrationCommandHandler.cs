using maintenance_calibration_system.Application.Abstract;
using maintenance_calibration_system.Application.MaintenanceActivity.Command.UpdateCalibration;
using maintenance_calibration_system.Contacts;
using maintenance_calibration_system.Domain.Datos_Historicos;


namespace maintenance_calibration_system.Application.MaintenanceActivity.Command.ModifyCalibration
{
    public class ModifyCalibrationCommandHandler(
    IMaintenanceActivityRepository<Calibration> calibrationRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<ModifyCalibrationCommand, bool>
    {
        private readonly IMaintenanceActivityRepository<Calibration> _calibrationRepository = calibrationRepository; // Repositorio para manejar calibraciones
        private readonly IUnitOfWork _unitOfWork = unitOfWork; // Unidad de trabajo para manejar transacciones

        public Task<bool> Handle(ModifyCalibrationCommand request, CancellationToken cancellationToken)
        {
            // Buscar la calibración existente
            var existingCalibration = _calibrationRepository.GetById(request.id);

            if (existingCalibration == null)
            {
                return Task.FromResult(false); // Retorna false si la calibración no existe
            }

            // Crear un nuevo objeto Calibration con los valores actualizados usando el constructor
            var modifyCalibration = new Calibration(
                existingCalibration.Id, // Mantener el mismo ID
                existingCalibration.DateActivity,
                existingCalibration.NameTechnician,
                request.CalibratedSensors,
                existingCalibration.NameCertificateAuthority);

            // Actualizar la calibración en el repositorio
            _calibrationRepository.Update(modifyCalibration);
            _unitOfWork.SaveChanges();

            return Task.FromResult(true); // Devuelve true si la actualización fue exitosa
        }
    }
}
