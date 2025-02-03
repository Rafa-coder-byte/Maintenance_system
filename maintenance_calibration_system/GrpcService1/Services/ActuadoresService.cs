using AutoMapper;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using GrpcService1;
using maintenance_calibration_system.Application.Equipments.Commands.CreateActuador; // Cambiado
using maintenance_calibration_system.Application.Equipments.Commands.DeleteActuador; // Cambiado
using maintenance_calibration_system.Application.Equipments.Commands.UpdateActuador; // Cambiado
using maintenance_calibration_system.Application.Equipments.Queries.GetAllActuador; // Cambiado
using maintenance_calibration_system.Application.Equipments.Queries.GetActuador; // Cambiado
using maintenance_calibration_system.Contacts;
using maintenance_calibration_system.GrpcProtos;
using MediatR;
using Microsoft.Extensions.Logging;

namespace GrpcService1.Services
{
    public class ActuadoresService( // Cambiado
        IMediator mediator,
        IMapper mapper,
        ILogger<ActuadoresService> logger,
        IEquipmentRepository<maintenance_calibration_system.Domain.Datos_de_Configuracion.Actuador> equipmentRepository, // Cambiado
        IUnitOfWork unitOfWork) : Actuador.ActuadorBase // Cambiado
    {
        private readonly IEquipmentRepository<maintenance_calibration_system.Domain.Datos_de_Configuracion.Actuador> _equipmentRepository = equipmentRepository; // Cambiado
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMediator _mediator = mediator;
        private readonly IMapper _mapper = mapper;
        private readonly ILogger<ActuadoresService> _logger = logger; // Inyectar el logger

        public override Task<ActuadorDTO> CreateActuador(CreateActuadorRequest request, ServerCallContext context) // Cambiado
        {
            var command = new CreateActuadorCommand( // Cambiado
                request.AlphanumericCode,
                new maintenance_calibration_system.Domain.ValueObjects.PhysicalMagnitude(
                    request.Magnitude.Name,
                    request.Magnitude.UnitofMagnitude),
                request.Manufacturer,
                request.CodeControl,
                (maintenance_calibration_system.Domain.Types.SignalControl)request.SignalControl);

            var result = _mediator.Send(command).Result;

            return Task.FromResult(_mapper.Map<ActuadorDTO>(result)); // Cambiado
        }

        public override Task<NullableActuadorDTO> GetActuador(GetRequest request, ServerCallContext context) // Cambiado
        {
            var query = new GetActuadorByIdQuery(new Guid(request.Id)); // Cambiado

            var result = _mediator.Send(query).Result;

            if (result == null)
            {
                _logger.LogWarning("Actuador no encontrado para ID: {ActuadorId}", request.Id); // Log de advertencia
                return Task.FromResult(new NullableActuadorDTO { Null = new Google.Protobuf.WellKnownTypes.Empty() });
            }
            else
            {
                _logger.LogInformation("Actuador encontrado para ID: {ActuadorId}", request.Id); // Log de información
            }

            return Task.FromResult(_mapper.Map<NullableActuadorDTO>(result)); // Cambiado
        }

        public override Task<Actuadores> GetAllActuadores(Google.Protobuf.WellKnownTypes.Empty request, ServerCallContext context) // Cambiado
        {
            var query = new GetAllActuadorQuery(); // Cambiado

            var result = _mediator.Send(query).Result;

            // Mapea la lista de Actuador a List<ActuadorDTO> // Cambiado
            var actuadorDTOs = _mapper.Map<List<ActuadorDTO>>(result); // Cambiado

            // Crea un nuevo objeto Actuadores y asigna la lista de ActuadorDTO // Cambiado
            var actuadoresResponse = new Actuadores // Cambiado
            {
                Items = { actuadorDTOs } // Asumiendo que Items es una colección repetida
            };

            return Task.FromResult(actuadoresResponse); // Devuelve el objeto Actuadores
        }

        public override Task<Empty> UpdateActuador(ActuadorDTO request, ServerCallContext context) // Cambiado
        {
            var command = new UpdateActuadorCommand( // Cambiado
                new Guid(request.Id),
                request.AlphanumericCode,
                new maintenance_calibration_system.Domain.ValueObjects.PhysicalMagnitude(
                    request.Magnitude.Name,
                    request.Magnitude.UnitofMagnitude),
                request.Manufacturer,
                request.CodeControl,
                (maintenance_calibration_system.Domain.Types.SignalControl)request.SignalControl);

            var result = _mediator.Send(command).Result;

            if (result)
            {
                context.ResponseTrailers.Add("status", "200");
                context.ResponseTrailers.Add("message", "Actuador updated successfully.");
            }
            else
            {
                context.ResponseTrailers.Add("status", "404");
                context.ResponseTrailers.Add("message", "Actuador ID not found.");
            }

            return Task.FromResult(new Empty());
        }

        public override Task<Empty> DeleteActuador(DeleteRequest request, ServerCallContext context) // Cambiado
        {
            var query = new DeleteActuadorCommand(new Guid(request.Id)); // Cambiado

            var result = _mediator.Send(query).Result;

            return Task.FromResult(new Empty());
        }
    }
}
