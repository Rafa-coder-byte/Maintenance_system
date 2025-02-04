using maintenance_calibration_system.DataAccess.Contexts;
using maintenance_calibration_system.Domain.Common;
using Microsoft.EntityFrameworkCore;

/// <summary>Interfaz genérica para un repositorio base que define las operaciones básicas de acceso a datos para cualquier tipo de entidad.</summary>
/// <typeparam name="T">El tipo de entidad que el repositorio manejará.</typeparam>
public interface IRepositoryBase<T> where T : Entity
{
    /// <summary>Añade una nueva entidad al repositorio.</summary>
    /// <param name="entity">La entidad a añadir.</param>
    void Add(T entity);

    /// <summary>Busca una entidad por su identificador único.</summary>
    /// <param name="id">El identificador único de la entidad.</param>
    /// <returns>La entidad correspondiente al identificador, o null si no se encuentra.</returns>
    T? GetById(Guid id);

    /// <summary>Devuelve todas las entidades del tipo especificado.</summary>
    /// <returns>Una colección de todas las entidades.</returns>
    IEnumerable<T> GetAll();

    /// <summary>Actualiza una entidad existente en el repositorio.</summary>
    /// <param name="entity">La entidad a actualizar.</param>
    void Update(T entity);

    /// <summary>Elimina una entidad del repositorio por su identificador único.</summary>
    /// <param name="id">El identificador único de la entidad a eliminar.</param>
    void Delete(Guid id);
}


/// <summary>Utiliza genéricos para permitir que cualquier tipo de entidad (que sea una clase) pueda ser manejada por el repositorio.</summary>
/// <remarks>El constructor recibe un contexto de aplicación (ApplicationContext), que se utiliza para interactuar con la base de datos.</remarks>
public abstract class RepositoryBase<T>(ApplicationContext context) : IRepositoryBase<T> where T : Entity
{
    protected readonly ApplicationContext _context = context;

    /// <summary>Añade una entidad al contexto y guarda los cambios.</summary>
    public void Add(T entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity), "La Sensor o actuador no puede ser nulo.");
        }
        _context.Set<T>().Add(entity);
        _context.SaveChanges();
    }

    /// <summary>Busca una entidad por su identificador.</summary>
    public virtual T? GetById(Guid id)
    {
        var entity = _context.Set<T>().Find(id);
        if (entity == null)
            return null;
        return entity;
    }

    /// <summary>Devuelve todas las entidades del tipo especificado.</summary>
    public virtual IEnumerable<T> GetAll()
    {
        return _context.Set<T>().ToList();
    }

    /// <summary>Actualiza una entidad existente en el contexto y guarda los cambios.</summary>
    public virtual void Update(T entity)
    {
        // Verificar si la entidad ya está siendo rastreada
        var localEntity = _context.Set<T>().Local.FirstOrDefault(e => e.Id == entity.Id);
        if (localEntity != null)
        {
            // Si la entidad ya está siendo rastreada, desadjuntarla
            _context.Entry(localEntity).State = EntityState.Detached;
        }

        // Ahora puedes adjuntar la nueva instancia
        _context.Set<T>().Update(entity);
        _context.SaveChanges();
    }

    /// <summary>Elimina una entidad por su identificador.</summary>
    public virtual void Delete(Guid id)
    {
        var entity = GetById(id);
        if (entity != null)
        {
            _context.Set<T>().Remove(entity);
            _context.SaveChanges();
        }
    }

}
