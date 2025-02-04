using maintenance_calibration_system.Domain.Datos_Historicos;
using maintenance_calibration_system.Domain.Types;
using maintenance_calibration_system.DataAccess.Contexts;
using maintenance_calibration_system.DataAccess.Tests.Utilities;
using maintenance_calibration_system.DataAccess.Respositories.MaintenanceActivitiy;
using maintenance_calibration_system.DataAccess.Respositories.Equipments;
using maintenance_calibration_system.Contacts;

namespace maintenance_calibration_system.Tests
{
    /// <summary>Clase de pruebas para las actividades de mantenimiento y calibración</summary>
    [TestClass]
    public class MaintenanceActivityTests
    {
        private ApplicationContext _context;
        private IUnitOfWork _unitOfWork;
        private MaintenanceActivityRepository<Maintenance> _maintenanceRepository;
        private MaintenanceActivityRepository<Calibration> _calibrationRepository;

        /// <summary>Inicializa el contexto y los repositorios</summary>
        [TestInitialize]
        public void SetUp()
        {
            _context = new ApplicationContext(ConnectionStringProvider.GetConnectingString());
            _unitOfWork = new UnitOfWork(_context);
            _maintenanceRepository = new MaintenanceActivityRepository<Maintenance>(_context);
            _calibrationRepository = new MaintenanceActivityRepository<Calibration>(_context);

            // Limpia y recrea la base de datos
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
        }

        /// <summary>Verifica que las propiedades de Calibration se inicialicen correctamente</summary>
        [TestMethod]
        public void Calibration_Creation_ShouldInitializeProperties()
        {
            // Arrange: Configura los datos de prueba
            var id = Guid.NewGuid();
            var dateActivity = DateTime.Now;
            var nameTechnician = "John Doe";
            var nameCertificateAuthority = "Cert Authority";

            // Act: Crea una nueva calibración
            var calibration = new Calibration(id, dateActivity, nameTechnician, nameCertificateAuthority);

            // Assert: Verifica que las propiedades se inicialicen correctamente
            Assert.AreEqual(id, calibration.Id);
            Assert.AreEqual(dateActivity, calibration.DateActivity);
            Assert.AreEqual(nameTechnician, calibration.NameTechnician);
            Assert.AreEqual(nameCertificateAuthority, calibration.NameCertificateAuthority);
            Assert.IsNotNull(calibration.CalibratedSensors);
            Assert.AreEqual(0, calibration.CalibratedSensors.Count);
        }

        /// <summary>Verifica que se pueda añadir una actividad de mantenimiento</summary>
        [TestMethod]
        public void AddMaintenance_ShouldAddMaintenance()
        {
            // Arrange: Configura los datos de prueba
            var id = Guid.NewGuid();
            var dateActivity = DateTime.Now;
            var nameTechnician = "Jane Doe";
            var typeMaintenance = TypeMaintenance.Preventivo;

            // Act: Añade una nueva actividad de mantenimiento
            var maintenance = new Maintenance(id, dateActivity, typeMaintenance, nameTechnician);
            _maintenanceRepository.Add(maintenance);
            _unitOfWork.SaveChanges();

            // Assert: Verifica que la actividad de mantenimiento se haya añadido
            var result = _context.Set<Maintenance>().FirstOrDefault(m => m.Id == id);
            Assert.IsNotNull(result);
            Assert.AreEqual(nameTechnician, result.NameTechnician);
        }

        /// <summary>Verifica que se pueda obtener una actividad de mantenimiento por ID</summary>
        [TestMethod]
        public void GetMaintenanceById_ShouldReturnMaintenance()
        {
            // Arrange: Configura los datos de prueba
            var id = Guid.NewGuid();
            var dateActivity = DateTime.Now;
            var nameTechnician = "Jane Doe";
            var typeMaintenance = TypeMaintenance.Preventivo;
            var maintenance = new Maintenance(id, dateActivity, typeMaintenance, nameTechnician);
            _context.Set<Maintenance>().Add(maintenance);
            _unitOfWork.SaveChanges();

            // Act: Obtiene la actividad de mantenimiento por ID
            var result = _maintenanceRepository.GetById(id);

            // Assert: Verifica que la actividad de mantenimiento se haya obtenido
            Assert.IsNotNull(result);
            Assert.AreEqual(nameTechnician, result.NameTechnician);
        }

        /// <summary>Verifica que se puedan obtener todas las actividades de mantenimiento</summary>
        [TestMethod]
        public void GetAllMaintenance_ShouldReturnAllMaintenance()
        {
            // Arrange: Configura los datos de prueba
            var maintenance1 = new Maintenance(Guid.NewGuid(), DateTime.Now, TypeMaintenance.Preventivo, "Technician A");
            var maintenance2 = new Maintenance(Guid.NewGuid(), DateTime.Now, TypeMaintenance.Correctivo, "Technician B");
            _context.Set<Maintenance>().Add(maintenance1);
            _context.Set<Maintenance>().Add(maintenance2);
            _unitOfWork.SaveChanges();

            // Act: Obtiene todas las actividades de mantenimiento
            var result = _maintenanceRepository.GetAll();

            // Assert: Verifica que se hayan obtenido todas las actividades de mantenimiento
            Assert.AreEqual(2, result.Count());
        }

        /// <summary>Verifica que se pueda actualizar una actividad de mantenimiento</summary>
        [TestMethod]
        public void UpdateMaintenance_ShouldModifyMaintenance()
        {
            // Arrange: Configura los datos de prueba
            var id = Guid.NewGuid();
            var dateActivity = DateTime.Now;
            var nameTechnician = "Jane Doe";
            var typeMaintenance = TypeMaintenance.Preventivo;
            var maintenance = new Maintenance(id, dateActivity, typeMaintenance, nameTechnician);
            _context.Set<Maintenance>().Add(maintenance);
            _unitOfWork.SaveChanges();

            // Act: Actualiza la actividad de mantenimiento
            maintenance.NameTechnician = "Updated Technician";
            _maintenanceRepository.Update(maintenance);
            _unitOfWork.SaveChanges();

            // Assert: Verifica que la actividad de mantenimiento se haya actualizado
            var result = _context.Set<Maintenance>().Find(id);
            Assert.AreEqual("Updated Technician", result.NameTechnician);
        }

        /// <summary>Verifica que se pueda eliminar una actividad de mantenimiento</summary>
        [TestMethod]
        public void DeleteMaintenance_ShouldRemoveMaintenance()
        {
            // Arrange: Configura los datos de prueba
            var id = Guid.NewGuid();
            var dateActivity = DateTime.Now;
            var nameTechnician = "Jane Doe";
            var typeMaintenance = TypeMaintenance.Preventivo;
            var maintenance = new Maintenance(id, dateActivity, typeMaintenance, nameTechnician);
            _context.Set<Maintenance>().Add(maintenance);
            _unitOfWork.SaveChanges();

            // Act: Elimina la actividad de mantenimiento
            _maintenanceRepository.Delete(id);
            _unitOfWork.SaveChanges();

            // Assert: Verifica que la actividad de mantenimiento se haya eliminado
            var result = _context.Set<Maintenance>().Find(id);
            Assert.IsNull(result);
        }

        /// <summary>Verifica que se pueda añadir una calibración</summary>
        [TestMethod]
        public void AddCalibration_ShouldAddCalibration()
        {
            // Arrange: Configura los datos de prueba
            var id = Guid.NewGuid();
            var dateActivity = DateTime.Now;
            var nameTechnician = "John Smith";
            var nameCertificateAuthority = "Momoa";

            // Act: Añade una nueva calibración
            var calibration = new Calibration(id, dateActivity, nameTechnician, nameCertificateAuthority);
            _calibrationRepository.Add(calibration);
            _unitOfWork.SaveChanges();

            // Assert: Verifica que la calibración se haya añadido
            var result = _context.Set<Calibration>().FirstOrDefault(c => c.Id == id);
            Assert.IsNotNull(result);
            Assert.AreEqual(nameTechnician, result.NameTechnician);
        }

        /// <summary>Verifica que se pueda obtener una calibración por ID</summary>
        [TestMethod]
        public void GetCalibrationById_ShouldReturnCalibration()
        {
            // Arrange: Configura los datos de prueba
            var id = Guid.NewGuid();
            var dateActivity = DateTime.Now;
            var nameTechnician = "John Smith";
            var nameCertificateAuthority = "Momoa";
            var calibration = new Calibration(id, dateActivity, nameTechnician, nameCertificateAuthority);
            _context.Set<Calibration>().Add(calibration);
            _unitOfWork.SaveChanges();

            // Act: Obtiene la calibración por ID
            var result = _calibrationRepository.GetById(id);

            // Assert: Verifica que la calibración se haya obtenido
            Assert.IsNotNull(result);
            Assert.AreEqual(nameTechnician, result.NameTechnician);
        }

            /// <summary>Verifica que se puedan obtener todas las calibraciones</summary>
         [TestMethod]
        public void GetAllCalibration_ShouldReturnAllCalibration()
        {
                // Arrange: Configura los datos de prueba
                var nameCertificateAuthority = "Momoa";
                var calibration1 = new Calibration(Guid.NewGuid(), DateTime.Now, "Technician C", nameCertificateAuthority);
                var calibration2 = new Calibration(Guid.NewGuid(), DateTime.Now, "Technician D", nameCertificateAuthority);
                _context.Set<Calibration>().AddRange(calibration1, calibration2);
                _unitOfWork.SaveChanges();

                // Act: Obtiene todas las calibraciones
                var result = _calibrationRepository.GetAll();

                // Assert: Verifica que se hayan obtenido todas las calibraciones
                Assert.AreEqual(2, result.Count());
        }

            /// <summary>Verifica que se pueda actualizar una calibración</summary>
       [TestMethod]
        public void UpdateCalibration_ShouldModifyCalibration()
        {
                // Arrange: Configura los datos de prueba
                var id = Guid.NewGuid();
                var dateActivity = DateTime.Now;
                var nameTechnician = "John Smith";
                var nameCertificateAuthority = "Momoa";
                var calibration = new Calibration(id, dateActivity, nameTechnician, nameCertificateAuthority);
                _context.Set<Calibration>().Add(calibration);
                _unitOfWork.SaveChanges();

                // Act: Actualiza la calibración
                calibration.NameTechnician = "Updated Technician";
                _calibrationRepository.Update(calibration);
                _unitOfWork.SaveChanges();

                // Assert: Verifica que la calibración se haya actualizado
                var result = _context.Set<Calibration>().Find(id);
                Assert.AreEqual("Updated Technician", result.NameTechnician);
        }

            /// <summary>Verifica que se pueda eliminar una calibración</summary>
        [TestMethod]
        public void DeleteCalibration_ShouldRemoveCalibration()
        {
                // Arrange: Configura los datos de prueba
                var id = Guid.NewGuid();
                var dateActivity = DateTime.Now;
                var nameTechnician = "John Smith";
                var nameCertificateAuthority = "Momoa";
                var calibration = new Calibration(id, dateActivity, nameTechnician, nameCertificateAuthority);
                _context.Set<Calibration>().Add(calibration);
                _unitOfWork.SaveChanges();

                // Act: Elimina la calibración
                _calibrationRepository.Delete(id);
                _unitOfWork.SaveChanges();

                // Assert: Verifica que la calibración se haya eliminado
                var result = _context.Set<Calibration>().Find(id);
                Assert.IsNull(result);
        }

    }
}
