namespace maintenance_calibration_system.Domain.Types
{
    /// <summary>Enumeraci�n de protocolos de comunicaci�n en el sistema de mantenimiento y calibraci�n.</summary>
    public enum CommunicationProtocol
    {
        /// <summary>Protocolo utilizado en automatizaci�n industrial.</summary>
        ModBus,

        /// <summary>Protocolo para interoperabilidad en automatizaci�n.</summary>
        OPC,

        /// <summary>OPC Unified Architecture, versi�n moderna del protocolo OPC.</summary>
        UA,

        /// <summary>Protocolo para automatizaci�n de edificios y control HVAC.</summary>
        BACNet,
    }
}
