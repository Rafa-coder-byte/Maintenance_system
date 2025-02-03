using maintenance_calibration_system.Domain.Datos_de_Planificación;
using MediatR;


namespace maintenance_calibration_system.Application.Plannings.Queries.GetPlanning
{
    public record GetPlanningByIdQuery(Guid Id) : IRequest<Planning?>;
}
