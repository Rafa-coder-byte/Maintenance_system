using maintenance_calibration_system.Application.Abstract;
using maintenance_calibration_system.Contacts;
using maintenance_calibration_system.Domain.Datos_Historicos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace maintenance_calibration_system.Application.MaintenanceActivity.Command.ModifyMaintenance
{
    public class ModifyMaintenanceCommandHandler(
    IMaintenanceActivityRepository<Maintenance> maintenanceRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<ModifyMaintenanceCommand, bool>
    {
        private readonly IMaintenanceActivityRepository<Maintenance> _maintenanceRepository = maintenanceRepository; // Repositorio para manejar calibraciones
        private readonly IUnitOfWork _unitOfWork = unitOfWork; // Unidad de trabajo para manejar transacciones

        public Task<bool> Handle(ModifyMaintenanceCommand request, CancellationToken cancellationToken)
        {
            // Buscar la calibración existente
            var existingMaintenance = _maintenanceRepository.GetById(request.id);

            if (existingMaintenance == null)
            {
                return Task.FromResult(false); // Retorna false si la calibración no existe
            }

            // Crear un nuevo objeto Calibration con los valores actualizados usando el constructor
            var modifyMaintenance = new Maintenance(
                existingMaintenance.Id, // Mantener el mismo ID
                existingMaintenance.DateActivity,
                existingMaintenance.TypeMaintenance,
                existingMaintenance.NameTechnician,
                request.MaintenanceActuador
                );

            // Actualizar la calibración en el repositorio
            _maintenanceRepository.Update(modifyMaintenance);
            _unitOfWork.SaveChanges();

            return Task.FromResult(true); // Devuelve true si la actualización fue exitosa
        }
    }
}
