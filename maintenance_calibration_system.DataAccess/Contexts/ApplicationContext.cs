using maintenance_calibration_system.Domain.Datos_de_Configuracion;
using maintenance_calibration_system.Domain.Datos_de_Planificación;
using maintenance_calibration_system.Domain.Datos_Historicos;
using Microsoft.EntityFrameworkCore;


namespace maintenance_calibration_system.DataAccess.Contexts
{
    /// <summary>Contexto de la aplicación para la base de datos SQLite.</summary>
    public class ApplicationContext : DbContext
    {
       

        /// <summary>Constructor por defecto.</summary>
        public ApplicationContext()
        {
        }

        /// <summary>Constructor con cadena de conexión.</summary>
        /// <param name="connectionString">Cadena de conexión a la base de datos.</param>
        public ApplicationContext(string connectionString)
            : base(GetOptions(connectionString))
        {
        }

        #region Helpers
        /// <summary>Obtiene las opciones de configuración para el contexto de la base de datos.</summary>
        /// <param name="connectionString">Cadena de conexión a la base de datos.</param>
        /// <returns>Opciones de configuración del contexto.</returns>
        private static DbContextOptions GetOptions(string connectionString)
        {
            return SqliteDbContextOptionsBuilderExtensions.UseSqlite(new DbContextOptionsBuilder(), connectionString).Options;
        }
        #endregion

        /// <summary>Constructor con opciones de configuración.</summary>
        /// <param name="options">Opciones de configuración del contexto.</param>
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
        }

        /// <summary>Configura el contexto de la base de datos.</summary>
        /// <param name="optionsBuilder">Constructor de opciones del contexto.</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlite("Data Source=maintenance_calibration_systemDb.sqlite");
        }

        /// <summary>Configura el modelo de la base de datos.</summary>
        /// <param name="modelBuilder">Constructor del modelo.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationContext).Assembly);
        }
    }
}

