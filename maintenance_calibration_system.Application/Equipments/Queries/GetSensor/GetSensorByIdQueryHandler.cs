using maintenance_calibration_system.Application.Abstract;
using maintenance_calibration_system.Contacts;
using maintenance_calibration_system.Domain.Datos_de_Configuracion;


namespace maintenance_calibration_system.Application.Equipments.Queries.GetSensor
{
    public class GetSensorByIdQueryHandler(IEquipmentRepository<Sensor?> equipmentRepository)
                : IQueryHandler<GetSensorByIdQuery, Sensor?>
    {
        private readonly IEquipmentRepository<Sensor>? _equipmentRepository = equipmentRepository;

        public Task<Sensor?> Handle(GetSensorByIdQuery request, CancellationToken cancellationToken)
        {
            // Obtener todos los sensores del repositorio
            Sensor? sensor = _equipmentRepository.GetById(request.Id);

            // Devolver null si no se encuentra el sensor
            if (sensor == null)
            {
                return Task.FromResult<Sensor?>(null);
            }
                return Task.FromResult(sensor);
        }
    }
}
