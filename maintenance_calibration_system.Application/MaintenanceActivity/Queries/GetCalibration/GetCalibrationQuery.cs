using maintenance_calibration_system.Domain.Datos_Historicos; // Asegúrate de que este espacio de nombres sea correcto
using MediatR;
using System;

namespace maintenance_calibration_system.Application.MaintenanceActivity.Queries.GetCalibration
{
    public record GetCalibrationByIdQuery(Guid Id) : IRequest<Calibration>; // Solicitud para obtener una calibración por ID
}
