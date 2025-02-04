using maintenance_calibration_system.Application.Abstract;
using maintenance_calibration_system.Contacts;
using maintenance_calibration_system.Domain.Datos_Historicos;// Asegúrate de que este espacio de nombres sea correcto
using Microsoft.Extensions.Logging;

namespace maintenance_calibration_system.Application.MaintenanceActivity.Command.UpdateCalibration
{
    public class UpdateCalibrationCommandHandler(
       IMaintenanceActivityRepository<Calibration> calibrationRepository,
        IUnitOfWork unitOfWork) : ICommandHandler<UpdateCalibrationCommand, bool>
    {
        private readonly IMaintenanceActivityRepository<Calibration> _calibrationRepository = (IMaintenanceActivityRepository<Calibration>)calibrationRepository; // Repositorio para manejar calibraciones
        private readonly IUnitOfWork _unitOfWork = unitOfWork; // Unidad de trabajo para manejar transacciones


        public Task<bool> Handle(UpdateCalibrationCommand request, CancellationToken cancellationToken)
        {
            // Buscar la calibración existente
            var existingCalibration = _calibrationRepository.GetById(request.id);

            if (existingCalibration == null)
            {
                return Task.FromResult(false); // Retorna false si la calibración no existe
            }

            // Crear un nuevo objeto Calibration con los valores actualizados usando el constructor
            var updatedCalibration = new Calibration(
                existingCalibration.Id, // Mantener el mismo ID
                request.DateActivity,
                request.NameTechnician,
                request.NameCertificateAuthority);


            // Actualizar la calibración en el repositorio
            _calibrationRepository.Update(updatedCalibration);
            _unitOfWork.SaveChanges();

            return Task.FromResult(true); // Devuelve true si la actualización fue exitosa
        }
    }
}
