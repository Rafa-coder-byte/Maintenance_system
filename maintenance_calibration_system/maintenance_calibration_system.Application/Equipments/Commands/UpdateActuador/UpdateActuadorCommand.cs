using maintenance_calibration_system.Application.Abstract;
using maintenance_calibration_system.Domain.Types;
using maintenance_calibration_system.Domain.ValueObjects;


namespace maintenance_calibration_system.Application.Equipments.Commands.UpdateActuador
{
    public record UpdateActuadorCommand(
         Guid Id,
         string AlphanumericCode,
         PhysicalMagnitude Magnitude,
         string Manufacturer,
         string CodeControl,
         SignalControl SignalControl) : ICommand<bool>;
}
