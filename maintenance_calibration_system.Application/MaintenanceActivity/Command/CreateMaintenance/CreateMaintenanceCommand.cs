using maintenance_calibration_system.Application.Abstract;
using maintenance_calibration_system.Domain.Datos_Historicos;
using maintenance_calibration_system.Domain.Datos_de_Configuracion;
using maintenance_calibration_system.Domain.Types; // Asegúrate de que este espacio de nombres tenga las definiciones necesarias
using System;
using maintenance_calibration_system.Application.Equipments.Queries.GetActuador;

namespace maintenance_calibration_system.Application.MaintenanceActivity.Command.CreateMaintenance
{
    public record CreateMaintenanceCommand(
        DateTime DateActivity,
        string NameTechnician,
        TypeMaintenance typeMaintenance,
        List<Actuador> MaintenanceActuators) : ICommand<Maintenance>;
}