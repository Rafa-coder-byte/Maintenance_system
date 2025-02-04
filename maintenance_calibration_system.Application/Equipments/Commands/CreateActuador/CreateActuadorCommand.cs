using maintenance_calibration_system.Application.Abstract;
using maintenance_calibration_system.Domain.Datos_de_Configuracion;
using maintenance_calibration_system.Domain.Types;
using maintenance_calibration_system.Domain.ValueObjects;


namespace maintenance_calibration_system.Application.Equipments.Commands.CreateActuador
{
    public record CreateActuadorCommand(
    string AlphanumericCode,
    PhysicalMagnitude Magnitude,
    string Manufacturer,
    string CodeControl,
    SignalControl SignalControl) : ICommand<Actuador>;
}
