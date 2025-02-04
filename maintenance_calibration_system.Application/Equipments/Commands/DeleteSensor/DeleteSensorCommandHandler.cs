using maintenance_calibration_system.Application.Abstract;
using maintenance_calibration_system.Contacts;
using maintenance_calibration_system.Domain.Datos_de_Configuracion;


namespace maintenance_calibration_system.Application.Equipments.Commands.DeleteSensor
{
    public class DeleteSensorCommandHandler(
        IEquipmentRepository<Sensor> equipmentRepository,
        IUnitOfWork unitOfWork) : ICommandHandler<DeleteSensorCommand, bool>
    {
        private readonly IEquipmentRepository<Sensor> _equipmentRepository = equipmentRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public Task<bool> Handle(DeleteSensorCommand request, CancellationToken cancellationToken)
        {
            bool result = true;
            _equipmentRepository.Delete(request.Id);
            _unitOfWork.SaveChanges();
            return Task.FromResult(result);
        }
    }
}
