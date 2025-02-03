using maintenance_calibration_system.Application.Abstract;
using maintenance_calibration_system.Domain.Datos_de_Configuracion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace maintenance_calibration_system.Application.MaintenanceActivity.Command.ModifyCalibration
{
    public record ModifyCalibrationCommand(
       Guid id,
       List<Sensor> CalibratedSensors) : ICommand<bool>;
}
