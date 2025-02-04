using maintenance_calibration_system.Application.Abstract;
using maintenance_calibration_system.Application.Equipments.Commands.UpdateActuador;
using maintenance_calibration_system.Contacts;
using maintenance_calibration_system.Contracts;
using maintenance_calibration_system.Domain.Datos_de_Planificación;
using Microsoft.Extensions.Logging;


namespace maintenance_calibration_system.Application.Plannings.Commands.UpdatePlanning
{
    public class UpdatePlanningCommandHandler(
        IPlanningRepository planningRepository,
        IUnitOfWork unitOfWork) : ICommandHandler<UpdatePlanningCommand, bool>
    {
        private readonly IPlanningRepository _planningRepository = planningRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;


        public Task<bool> Handle(UpdatePlanningCommand request, CancellationToken cancellationToken)
        {
            // Buscar el sensor existente
            var existingPlanning = _planningRepository.GetById(request.Id);

            if (existingPlanning == null)
            {
                return Task.FromResult(false); // Devuelve false si no se encuentra el sensor
            }

            // Crear un nuevo objeto Sensor con los valores actualizados usando el constructor
            var updatedPlanning = new Planning(
                existingPlanning.Id, // Mantener el mismo ID
                request.EquipmentElement,
                request.Type,
                request.ExecutionDate); // Asegúrate de que esta propiedad esté presente


            // Actualizar el sensor en el repositorio
            _planningRepository.Update(updatedPlanning);
            _unitOfWork.SaveChanges();

            return Task.FromResult(true); // Devuelve true si la actualización fue exitosa
        }
    }
}

