using maintenance_calibration_system.Application.Abstract;
using maintenance_calibration_system.Contacts;
using maintenance_calibration_system.Domain.Datos_de_Configuracion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace maintenance_calibration_system.Application.Equipments.Queries.GetAllSensor
{
    public class GetAllSensorQueryHandler(IEquipmentRepository<Sensor> equipmentRepository)
                : IQueryHandler<GetAllSensorQuery, List<Sensor>>
    {
        private readonly IEquipmentRepository<Sensor> _equipmentRepository = equipmentRepository;

        public Task<List<Sensor>> Handle(GetAllSensorQuery request, CancellationToken cancellationToken)
        {
            // Obtener todos los sensores del repositorio
            var sensors = _equipmentRepository.GetAll();

            return Task.FromResult(sensors.ToList());
        }
    }
}
