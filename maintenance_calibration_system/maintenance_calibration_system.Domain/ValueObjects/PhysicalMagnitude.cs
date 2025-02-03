using maintenance_calibration_system.Domain.Common;
using System.Xml.Linq;

namespace maintenance_calibration_system.Domain.ValueObjects
{
    /// <summary>Representa una magnitud física en el sistema.</summary>
    public class PhysicalMagnitude : ValueObject
    {
        /// <summary>Nombre de la magnitud física.</summary>
        public string Name { get; set; }

        /// <summary>Unidad de la magnitud física.</summary>
        public string UnitofMagnitude { get; set; }

        /// <summary>Constructor por defecto.</summary>
        public PhysicalMagnitude()
        {
            Name = "Default1";
            UnitofMagnitude = "Default2";
        }

        /// <summary>Constructor para crear una instancia de PhysicalMagnitude.</summary>
        /// <param name="name">Nombre de la magnitud física.</param>
        /// <param name="magnitude">Unidad de la magnitud física.</param>
        public PhysicalMagnitude(string name, string magnitude)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name)); // Lanza excepción si name es nulo;
            UnitofMagnitude = magnitude ?? throw new ArgumentNullException(nameof(magnitude)); // Lanza excepción si name es nulo;;
        }

        /// <summary>Obtiene los componentes de igualdad para la magnitud física.</summary>
        /// <returns>Componentes de igualdad.</returns>
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Name;
            yield return UnitofMagnitude;
        }
    }
}
