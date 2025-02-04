using maintenance_calibration_system.Application.Abstract;
using maintenance_calibration_system.Contacts;
using maintenance_calibration_system.Contracts;



namespace maintenance_calibration_system.Application.Plannings.Commands.DeletePlanning
{
    public class DeletePlanningCommandHandler(
        IPlanningRepository planningRepository,
        IUnitOfWork unitOfWork) : ICommandHandler<DeletePlanningCommand, bool>
    {
        private readonly IPlanningRepository _planningRepository = planningRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public Task<bool> Handle(DeletePlanningCommand request, CancellationToken cancellationToken)
        {
            bool result = true;
            _planningRepository.Delete(request.Id);
            _unitOfWork.SaveChanges();
            return Task.FromResult(result);
        }
    }
}
