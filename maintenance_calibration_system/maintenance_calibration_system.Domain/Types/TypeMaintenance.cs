namespace maintenance_calibration_system.Domain.Types
{
    /// <summary>Tipos de mantenimiento en el sistema de mantenimiento y calibración.</summary>
    public enum TypeMaintenance
    {
        /// <summary>Mantenimiento realizado de manera programada para prevenir fallos.</summary>
        Preventivo,

        /// <summary>Mantenimiento realizado para corregir fallos detectados.</summary>
        Correctivo,
    }
}
