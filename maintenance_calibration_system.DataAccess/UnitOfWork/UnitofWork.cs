using maintenance_calibration_system.Contacts;
using maintenance_calibration_system.DataAccess.Contexts;
using Microsoft.EntityFrameworkCore;


/// <summary>Implementación del patrón UnitOfWork.</summary>
public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationContext _context;

    /// <summary>Constructor que inicializa el contexto de la aplicación.</summary>
    /// <param name="context">El contexto de la aplicación.</param>
    public UnitOfWork(ApplicationContext context)
    {
        _context = context;
        // Verifica si la base de datos puede conectarse, si no, aplica migraciones pendientes
        if (!context.Database.CanConnect())
            context.Database.Migrate();

    }

    /// <summary>Guarda los cambios realizados en el contexto.</summary>
    public void SaveChanges()
    {
        _context.SaveChanges();
    }
}
