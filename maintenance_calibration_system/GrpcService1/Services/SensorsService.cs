using AutoMapper;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using GrpcService1;
using maintenance_calibration_system.Application.Equipments.Commands.CreateSensor;
using maintenance_calibration_system.Application.Equipments.Commands.DeleteSensor;
using maintenance_calibration_system.Application.Equipments.Commands.UpdateSensor;
using maintenance_calibration_system.Application.Equipments.Queries.GetAllSensor;
using maintenance_calibration_system.Application.Equipments.Queries.GetSensor;
using maintenance_calibration_system.Contacts;
using maintenance_calibration_system.GrpcProtos;
using MediatR;
using Microsoft.Extensions.Logging;

namespace GrpcService1.Services
{
    public class SensorsService(
        IMediator mediator,
        IMapper mapper,
        ILogger <SensorsService> logger,
        IEquipmentRepository<maintenance_calibration_system.Domain.Datos_de_Configuracion.Sensor> equipmentRepository,
        IUnitOfWork unitOfWork) : Sensor.SensorBase
    {
        private readonly IEquipmentRepository<maintenance_calibration_system.Domain.Datos_de_Configuracion.Sensor> _equipmentRepository = equipmentRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMediator _mediator = mediator; 
        private readonly IMapper _mapper = mapper;
        private readonly ILogger <SensorsService> _logger = logger;

        public override Task<SensorDTO> CreateSensor(CreateSensorRequest request, ServerCallContext context)
        {
            var command = new CreateSensorCommand(
                request.AlphanumericCode,
                new maintenance_calibration_system.Domain.ValueObjects.PhysicalMagnitude(
                    request.Magnitude.Name,
                    request.Magnitude.UnitofMagnitude),
                request.Manufacturer,
                (maintenance_calibration_system.Domain.Types.CommunicationProtocol)request.Protocol,
                request.PrincipleOperation);

            var result = _mediator.Send(command).Result;

            return Task.FromResult(_mapper.Map<SensorDTO>(result));
        }

        public override Task<NullableSensorDTO> GetSensor(GetRequest request, ServerCallContext context)
        {
            
                var query = new GetSensorByIdQuery(new Guid(request.Id));
                var result =  _mediator.Send(query).Result;

                if (result == null)
                {
                    _logger.LogWarning("Sensor with ID {SensorId} not found.", request.Id);
                  return Task.FromResult(new NullableSensorDTO { Null = new Google.Protobuf.WellKnownTypes.Empty() });

                }

            else
                {
                    _logger.LogInformation("Actuador encontrado para ID: {SensorId}", request.Id); // Log de información
                }

                return Task.FromResult(_mapper.Map<NullableSensorDTO>(result));
        }


        public override Task<Sensors> GetAllSensors(Google.Protobuf.WellKnownTypes.Empty request, ServerCallContext context)
        {
            var query = new GetAllSensorQuery();

            var result = _mediator.Send(query).Result;

            // Mapea la lista de Sensor a List<SensorDTO>
            var sensorDTOs = _mapper.Map<List<SensorDTO>>(result);

            // Crea un nuevo objeto Sensors y asigna la lista de SensorDTO
            var sensorsResponse = new Sensors
            {
                Items = { sensorDTOs } // Asumiendo que Items es una colección repetida
            };

            return Task.FromResult(sensorsResponse); // Devuelve el objeto Sensors
        }

        public override Task<Empty> UpdateSensor(SensorDTO request, ServerCallContext context)
        {
            var command = new UpdateSensorCommand(
                new Guid(request.Id),
                request.AlphanumericCode,
                new maintenance_calibration_system.Domain.ValueObjects.PhysicalMagnitude(
                    request.Magnitude.Name,
                    request.Magnitude.UnitofMagnitude),
                request.Manufacturer,
                (maintenance_calibration_system.Domain.Types.CommunicationProtocol)request.Protocol,
                request.PrincipleOperation);

            var result = _mediator.Send(command).Result;

            if (result)
            {
                context.ResponseTrailers.Add("status", "200");
                context.ResponseTrailers.Add("message", "Sensor updated successfully.");
            }
            else
            {
                context.ResponseTrailers.Add("status", "404");
                context.ResponseTrailers.Add("message", "Sensor ID not found.");
            }

            return Task.FromResult(new Empty());
        }

        public override Task<Empty> DeleteSensor(DeleteRequest request, ServerCallContext context)
        {
            var query = new DeleteSensorCommand(new Guid(request.Id));

            var result = _mediator.Send(query).Result;

            return Task.FromResult(new Empty());
        }
    }
}
