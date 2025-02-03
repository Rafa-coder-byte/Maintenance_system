using System;
using maintenance_calibration_system.Application.Abstract;
using maintenance_calibration_system.Domain.Datos_de_Configuracion;
using maintenance_calibration_system.Domain.Types; // Asegúrate de que este espacio de nombres sea correcto
using maintenance_calibration_system.Domain.ValueObjects; // Asegúrate de que este espacio de nombres sea correcto

namespace maintenance_calibration_system.Application.MaintenanceActivity.Command.UpdateCalibration
{
    public record UpdateCalibrationCommand(
        Guid id, // Identificador único de la calibración
        DateTime DateActivity, // Fecha de la actividad de calibración
        string NameTechnician, // Nombre del técnico que realizó la calibración
        string NameCertificateAuthority, // Nombre de la autoridad certificadora
        List<Sensor> CalibratedSensors) : ICommand<bool>;// Lista de sensores calibrados
}
