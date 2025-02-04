using maintenance_calibration_system.Application.Abstract;
using maintenance_calibration_system.Domain.Types;


namespace maintenance_calibration_system.Application.Plannings.Commands.UpdatePlanning
{
    public record UpdatePlanningCommand(
         Guid Id,
         string EquipmentElement,
         PlanningTypes Type,
         DateTime ExecutionDate) : ICommand<bool>;
}
