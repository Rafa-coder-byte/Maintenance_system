using maintenance_calibration_system.Application.Abstract;


namespace maintenance_calibration_system.Application.Plannings.Commands.DeletePlanning
{
    public record DeletePlanningCommand(Guid Id) : ICommand<bool>;
}
