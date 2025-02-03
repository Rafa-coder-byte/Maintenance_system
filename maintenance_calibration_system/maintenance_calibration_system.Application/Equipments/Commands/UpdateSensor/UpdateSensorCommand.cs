using maintenance_calibration_system.Application.Abstract;
using maintenance_calibration_system.Domain.Types;
using maintenance_calibration_system.Domain.ValueObjects;


namespace maintenance_calibration_system.Application.Equipments.Commands.UpdateSensor
{
    public record UpdateSensorCommand(
         Guid Id,
         string AlphanumericCode,
         PhysicalMagnitude Magnitude,
         string Manufacturer,
         CommunicationProtocol Protocol,
         string PrincipleOperation) : ICommand<bool>;
}
