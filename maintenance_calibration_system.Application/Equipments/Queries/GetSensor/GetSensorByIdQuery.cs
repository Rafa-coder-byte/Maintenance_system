using maintenance_calibration_system.Domain.Datos_de_Configuracion;
using MediatR;


namespace maintenance_calibration_system.Application.Equipments.Queries.GetSensor
{
    public record GetSensorByIdQuery(Guid Id) : IRequest<Sensor>;
}
