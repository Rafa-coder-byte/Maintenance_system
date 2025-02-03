

namespace maintenance_calibration_system.Domain.Common 
{
    public abstract class ValueObject // Clase base abstracta para objetos de valor.
    {
        protected abstract IEnumerable<object> GetEqualityComponents(); // Método abstracto para obtener componentes de igualdad.

        public override bool Equals(object? obj) // Sobrescribe el método Equals para comparar objetos.
        {
            if (obj == null || obj.GetType() != GetType()) // Verifica si el objeto es nulo o de diferente tipo.
                return false;

            var other = (ValueObject)obj; // Convierte el objeto a ValueObject.

            return this.GetEqualityComponents().SequenceEqual(other.GetEqualityComponents()); // Compara los componentes de igualdad.
        }

        public override int GetHashCode() // Sobrescribe el método GetHashCode.
        {
            throw new NotImplementedException(); // Lanza una excepción si no se implementa.
        }
    }
}