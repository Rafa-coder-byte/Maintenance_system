using maintenance_calibration_system.Application.Abstract;
using maintenance_calibration_system.Contacts;
using maintenance_calibration_system.Domain.Datos_de_Configuracion;


namespace maintenance_calibration_system.Application.Equipments.Commands.CreateActuador
{
    public class CreateActuadorCommandHandler(
        IEquipmentRepository<Actuador> equipmentRepository,
        IUnitOfWork unitOfWork)
                : ICommandHandler<CreateActuadorCommand, Actuador>
    {

        private readonly IEquipmentRepository<Actuador> _equipmentRepository = equipmentRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public Task<Actuador> Handle(CreateActuadorCommand request, CancellationToken cancellationToken)
        {
            Actuador result = new Actuador(
                Guid.NewGuid(),
                request.AlphanumericCode,
                request.Magnitude,
                request.Manufacturer,
                request.CodeControl,
                request.SignalControl);

            _equipmentRepository.Add(result);
            _unitOfWork.SaveChanges();

            return Task.FromResult(result);
        }

    }
}
