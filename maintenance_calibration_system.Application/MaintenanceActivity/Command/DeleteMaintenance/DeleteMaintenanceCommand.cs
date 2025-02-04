using maintenance_calibration_system.Application.Abstract;


namespace maintenance_calibration_system.Application.MaintenanceActivity.Command.DeleteMaintenance
{
    public record DeleteMaintenanceCommand(Guid Id) : ICommand<bool>;
}
