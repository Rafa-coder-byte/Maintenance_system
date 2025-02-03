using maintenance_calibration_system.Application.Abstract;


namespace maintenance_calibration_system.Application.MaintenanceActivity.Command.DeleteCalibration
{
    public record DeleteCalibrationCommand(Guid Id) : ICommand<bool>;
}