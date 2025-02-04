using maintenance_calibration_system.Domain.Datos_Historicos; // Asegúrate de que este espacio de nombres sea correcto
using MediatR;


namespace maintenance_calibration_system.Application.MaintenanceActivity.Queries.GetCalibration
{
    public record GetMaintenanceByIdQuery(Guid Id) : IRequest<Maintenance>; // Solicitud para obtener un mantenimiento por ID
}
