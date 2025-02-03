using maintenance_calibration_system.Domain.Datos_de_Planificación; // Para Planning
using maintenance_calibration_system.Domain.Types;
using maintenance_calibration_system.DataAccess.Respositories.Plannings;
using maintenance_calibration_system.DataAccess.Contexts;
using maintenance_calibration_system.DataAccess.Tests.Utilities;
using maintenance_calibration_system.Contacts;
using maintenance_calibration_system.Contracts; // Para PlanningRepository

namespace maintenance_calibration_system.DataAccess.Tests
{
    /// <summary>Clase de pruebas unitarias para el repositorio de Planificaciones.</summary>
    [TestClass]
    public class PlanningTests
    {
        private IPlanningRepository planningRepository; // Instancia del repositorio a probar
        private IUnitOfWork unitOfWork; // Instancia del UnitOfWork
        private ApplicationContext _context;

        /// <summary>Constructor que inicializa el contexto y los repositorios.</summary>
        [TestInitialize]
        public void SetUp()
        {
            _context = new ApplicationContext(ConnectionStringProvider.GetConnectingString()); // Asigna a _context
            unitOfWork = new UnitOfWork(_context); // Inicializa el UnitOfWork
            planningRepository = new PlanningRepository(_context); // Inicializa el repositorio

            // Limpia y recrea la base de datos
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
        }

        /// <summary>Prueba para verificar que Add agrega una planificación.</summary>
        [TestMethod]
        public void Add_ShouldAddPlanning()
        {
            // Arrange: Configura los datos de prueba
            var planning = new Planning { Id = Guid.NewGuid(), EquipmentElement = "Sensor1", Type = PlanningTypes.Maintenance, ExecutionDate = DateTime.Now };

            // Act: Añade la planificación al repositorio y guarda los cambios
            planningRepository.Add(planning);
            unitOfWork.SaveChanges();

            // Assert: Verifica que la planificación se haya añadido correctamente
            var result = planningRepository.GetById(planning.Id);
            Assert.IsNotNull(result); // Verifica que la planificación no sea nula
            Assert.AreEqual(planning.EquipmentElement, result.EquipmentElement); // Verifica que los datos sean correctos
            Assert.AreEqual(planning.Type, result.Type);
            Assert.AreEqual(planning.ExecutionDate.Date, result.ExecutionDate.Date); // Compara solo la fecha
        }

        /// <summary>Prueba para verificar que GetById devuelve una planificación cuando existe.</summary>
        [TestMethod]
        public void GetById_ShouldReturnPlanning_WhenExists()
        {
            // Arrange: Configura los datos de prueba
            var planningId = Guid.NewGuid();
            var planning = new Planning { Id = planningId, EquipmentElement = "Sensor1", Type = PlanningTypes.Maintenance, ExecutionDate = DateTime.Now };
            planningRepository.Add(planning);
            unitOfWork.SaveChanges();

            // Act: Obtiene la planificación por ID desde el repositorio
            var result = planningRepository.GetById(planningId);

            // Assert: Verifica que la planificación se haya obtenido correctamente
            Assert.IsNotNull(result); // Verifica que no sea nulo
            Assert.AreEqual(planningId, result.Id); // Verifica que el ID sea correcto
        }

        /// <summary>Prueba para verificar que GetAll devuelve todas las planificaciones.</summary>
        [TestMethod]
        public void GetAll_ShouldReturnAllPlannings()
        {
            // Arrange: Configura los datos de prueba
            var planning1 = new Planning { Id = Guid.NewGuid(), EquipmentElement = "Sensor1", Type = PlanningTypes.Calibration, ExecutionDate = DateTime.Now };
            var planning2 = new Planning { Id = Guid.NewGuid(), EquipmentElement = "Sensor2", Type = PlanningTypes.Maintenance, ExecutionDate = DateTime.Now.AddDays(1) };
            planningRepository.Add(planning1);
            planningRepository.Add(planning2);
            unitOfWork.SaveChanges();

            // Act: Obtiene todas las planificaciones desde el repositorio
            var result = planningRepository.GetAll();

            // Assert: Verifica que se hayan obtenido todas las planificaciones
            Assert.AreEqual(2, result.Count()); // Verifica que se devuelvan dos planificaciones
        }

        /// <summary>Prueba para verificar que Update modifica una planificación existente.</summary>
        [TestMethod]
        public void Update_ShouldUpdateExistingPlanning()
        {
            // Arrange: Configura los datos de prueba
            var planning = new Planning { Id = Guid.NewGuid(), EquipmentElement = "OldSensor", Type = PlanningTypes.Calibration, ExecutionDate = DateTime.Now };
            planningRepository.Add(planning);
            unitOfWork.SaveChanges();

            var updatedPlanning = new Planning { Id = planning.Id, EquipmentElement = "NewSensor", Type = PlanningTypes.Maintenance, ExecutionDate = DateTime.Now.AddDays(2) };

            // Act: Actualiza la planificación en el repositorio y guarda los cambios
            planningRepository.Update(updatedPlanning);
            unitOfWork.SaveChanges();

            // Assert: Verifica que la planificación se haya actualizado correctamente
            var result = planningRepository.GetById(planning.Id);
            Assert.AreEqual("NewSensor", result.EquipmentElement); // Verifica que se haya actualizado correctamente
            Assert.AreEqual(PlanningTypes.Maintenance, result.Type);
            Assert.AreEqual(updatedPlanning.ExecutionDate.Date, result.ExecutionDate.Date); // Compara solo la fecha sin la hora
        }

        /// <summary>Prueba para verificar que Delete elimina una planificación cuando existe.</summary>
        [TestMethod]
        public void Delete_ShouldRemovePlanning_WhenExists()
        {
            // Arrange: Configura los datos de prueba
            var planningId = Guid.NewGuid();
            var planning = new Planning { Id = planningId, EquipmentElement = "Sensor1", Type = PlanningTypes.Calibration, ExecutionDate = DateTime.Now };
            planningRepository.Add(planning);
            unitOfWork.SaveChanges();

            // Act: Elimina la planificación del repositorio y guarda los cambios
            planningRepository.Delete(planningId);
            unitOfWork.SaveChanges();

            // Assert: Verifica que la planificación se haya eliminado correctamente
            var result = planningRepository.GetById(planningId);
            Assert.IsNull(result); // Verifica que la planificación haya sido eliminada
        }
    }
}
