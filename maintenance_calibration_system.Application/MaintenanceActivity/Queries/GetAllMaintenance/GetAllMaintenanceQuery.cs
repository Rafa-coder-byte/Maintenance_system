using maintenance_calibration_system.Domain.Datos_Historicos; // Asegúrate de que este espacio de nombres sea correcto
using MediatR;
using System.Collections.Generic;

namespace maintenance_calibration_system.Application.MaintenanceActivity.Queries.GetAllCalibration
{
    public record GetAllMaintenanceQuery : IRequest<List<Maintenance>>; // Solicitud para obtener todas los mantenimientos
}