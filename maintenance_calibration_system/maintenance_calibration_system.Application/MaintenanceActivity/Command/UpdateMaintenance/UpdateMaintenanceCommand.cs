using System;
using maintenance_calibration_system.Application.Abstract;
using maintenance_calibration_system.Domain.Datos_Historicos;
using maintenance_calibration_system.Domain.Datos_de_Configuracion;
using maintenance_calibration_system.Domain.Types; // Asegúrate de que este espacio de nombres sea correcto


namespace maintenance_calibration_system.Application.MaintenanceActivity.Command.UpdateMaintenance
{
    public record UpdateMaintenanceCommand(
        Guid id,  //Identificador unico del mantenimiento
        DateTime DateActivity, //fecha de la actividad de mantenimiento
        string NameTechnician, //Nombre del tecnico que realizo el mantenimiento
        TypeMaintenance TypeMaintenance,//Tipo de mantenimiento dado
        List<Actuador> MaintenanceActuators) : ICommand<bool>; //Lista de actuadores con mantenimiento
}
