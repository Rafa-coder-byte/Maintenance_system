namespace maintenance_calibration_system.Domain.Types
{
    /// <summary>Enumeración de protocolos de comunicación en el sistema de mantenimiento y calibración.</summary>
    public enum CommunicationProtocol
    {
        /// <summary>Protocolo utilizado en automatización industrial.</summary>
        ModBus,

        /// <summary>Protocolo para interoperabilidad en automatización.</summary>
        OPC,

        /// <summary>OPC Unified Architecture, versión moderna del protocolo OPC.</summary>
        UA,

        /// <summary>Protocolo para automatización de edificios y control HVAC.</summary>
        BACNet,
    }
}
