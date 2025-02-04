using maintenance_calibration_system.Application.Abstract;
using maintenance_calibration_system.Domain.Datos_de_Configuracion;
using maintenance_calibration_system.Domain.Types;
using maintenance_calibration_system.Domain.ValueObjects;


namespace maintenance_calibration_system.Application.Equipments.Commands.CreateSensor
{
    public record CreateSensorCommand(
    string AlphanumericCode,
    PhysicalMagnitude Magnitude,
    string Manufacturer,
    CommunicationProtocol Protocol,
    string PrincipleOperation) : ICommand<Sensor>;
}
