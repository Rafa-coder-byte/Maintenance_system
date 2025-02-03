using maintenance_calibration_system.Application.Abstract;

namespace maintenance_calibration_system.Application.Equipments.Commands.DeleteSensor
{
    public record DeleteSensorCommand(Guid Id) : ICommand<bool>;
}
