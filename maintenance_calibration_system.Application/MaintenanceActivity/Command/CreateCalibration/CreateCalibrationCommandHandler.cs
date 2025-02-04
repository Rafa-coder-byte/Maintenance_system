using maintenance_calibration_system.Application.Abstract;
using maintenance_calibration_system.Contacts;
using maintenance_calibration_system.Domain.Datos_Historicos;
using maintenance_calibration_system.Domain.Datos_de_Configuracion; // Asegúrate de que este espacio de nombres sea correcto
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace maintenance_calibration_system.Application.MaintenanceActivity.Command.CreateCalibration
{
    public class CreateCalibrationCommandHandler(
       IMaintenanceActivityRepository<Calibration> calibrationRepository,
        IUnitOfWork unitOfWork)
                : ICommandHandler<CreateCalibrationCommand, Calibration>
    {
        private readonly IMaintenanceActivityRepository<Calibration> _calibrationRepository = (IMaintenanceActivityRepository<Calibration>)calibrationRepository; // Cambiado para usar el repositorio de calibraciones
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public Task<Calibration> Handle(CreateCalibrationCommand request, CancellationToken cancellationToken)
        {
            var result = new Calibration(
                Guid.NewGuid(),
                request.DateActivity,
                request.NameTechnician,
                request.NameCertificateAuthority);


            _calibrationRepository.Add(result);
            _unitOfWork.SaveChanges();

            return Task.FromResult(result);

        }
    }
}

