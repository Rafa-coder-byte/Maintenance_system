using maintenance_calibration_system.Domain.Datos_de_Configuracion;
using MediatR;


namespace maintenance_calibration_system.Application.Equipments.Queries.GetActuador
{
    public record GetActuadorByIdQuery(Guid Id) : IRequest<Actuador>;
}
