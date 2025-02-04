using maintenance_calibration_system.Application.Abstract;
using maintenance_calibration_system.Domain.Datos_de_Planificación;
using maintenance_calibration_system.Domain.Types;


namespace maintenance_calibration_system.Application.Plannings.Commands.CreatePlanning
{
    public record CreatePlanningCommand(
    string EquipmentElement,
    PlanningTypes Type,
    DateTime ExecutionDate) : ICommand<Planning>;
}
