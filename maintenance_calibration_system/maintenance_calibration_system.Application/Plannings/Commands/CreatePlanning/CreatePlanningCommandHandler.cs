using maintenance_calibration_system.Application.Abstract;
using maintenance_calibration_system.Contacts;
using maintenance_calibration_system.Contracts;
using maintenance_calibration_system.Domain.Datos_de_Planificación;


namespace maintenance_calibration_system.Application.Plannings.Commands.CreatePlanning
{
    public class CreatePlanningCommandHandler(
        IPlanningRepository planningRepository,
        IUnitOfWork unitOfWork)
                : ICommandHandler<CreatePlanningCommand, Planning>
    {

        private readonly IPlanningRepository _planningRepository = planningRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public Task<Planning> Handle(CreatePlanningCommand request, CancellationToken cancellationToken)
        {
            Planning result = new Planning(
                Guid.NewGuid(),
                request.EquipmentElement,
                request.Type,
                request.ExecutionDate);

            _planningRepository.Add(result);
            _unitOfWork.SaveChanges();

            return Task.FromResult(result);
        }

    }
}
