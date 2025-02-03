using maintenance_calibration_system.Contacts;
using maintenance_calibration_system.DataAccess.Contexts;
using maintenance_calibration_system.DataAccess.Respositories.Equipments;
using maintenance_calibration_system.DataAccess.Tests.Utilities;
using maintenance_calibration_system.Domain.Datos_de_Configuracion;
using maintenance_calibration_system.Domain.Types;
using maintenance_calibration_system.Domain.ValueObjects;


namespace maintenance_calibration_system.Tests.DataAccess.Repositories.Equipments
{
    /// <summary>Clase de pruebas unitarias para EquipmentRepository.</summary>
    [TestClass]
    public class EquipmentRepositoryTests
    {
        private ApplicationContext _context;
        private IUnitOfWork _unitOfWork;
        private EquipmentRepository<Sensor> _sensorRepository;
        private EquipmentRepository<Actuador> _actuatorRepository;

        /// <summary>Constructor que inicializa el contexto y los repositorios.</summary>
        [TestInitialize]
        public void SetUp()
        {
            _context = new ApplicationContext(ConnectionStringProvider.GetConnectingString());
            _unitOfWork = new UnitOfWork(_context); // Inicializa la unidad de trabajo
            _sensorRepository = new EquipmentRepository<Sensor>(_context);
            _actuatorRepository = new EquipmentRepository<Actuador>(_context);

            // Limpia y recrea la base de datos
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
        }

        /// <summary>Prueba para verificar que Add agrega un sensor.</summary>
        [TestMethod]
        public void Add_ShouldAddSensor()
        {
            // Arrange
            var someMagnitude = new PhysicalMagnitude("Temperature", "Celsius");
            var sensor = new Sensor(Guid.NewGuid(), "SENSOR001", someMagnitude, "ManufacturerA", CommunicationProtocol.UA, "PrincipleA");

            // Act
            _sensorRepository.Add(sensor);
            _unitOfWork.SaveChanges(); 

            // Assert
            var result = _context.Set<Sensor>().FirstOrDefault(s => s.AlphanumericCode == "SENSOR001");
            Assert.IsNotNull(result);
            Assert.AreEqual("ManufacturerA", result.Manufacturer);
        }

        /// <summary>Prueba para verificar que GetById devuelve un sensor.</summary>
        [TestMethod]
        public void GetById_ShouldReturnSensor()
        {
            // Arrange
            var someMagnitude = new PhysicalMagnitude("Temperature", "Celsius");
            var sensor = new Sensor(Guid.NewGuid(), "SENSOR002", someMagnitude, "ManufacturerB", CommunicationProtocol.UA, "PrincipleB");

            _context.Set<Sensor>().Add(sensor);
            _unitOfWork.SaveChanges(); 

            // Act
            var result = _sensorRepository.GetById(sensor.Id);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("SENSOR002", result.AlphanumericCode);
        }

        /// <summary>Prueba para verificar que GetAll devuelve todos los sensores.</summary>
        [TestMethod]
        public void GetAll_ShouldReturnAllSensors()
        {
            // Arrange
            var someMagnitude = new PhysicalMagnitude("Temperature", "Celsius");
            var sensor1 = new Sensor(Guid.NewGuid(), "SENSOR003", someMagnitude, "ManufacturerC", CommunicationProtocol.UA, "PrincipleC");
            var SomeMagnitude = new PhysicalMagnitude("Temperature", "Celsius");
            var sensor2 = new Sensor(Guid.NewGuid(), "SENSOR004", SomeMagnitude, "ManufacturerD", CommunicationProtocol.UA, "PrincipleD");

            _context.Set<Sensor>().Add(sensor1);
            _unitOfWork.SaveChanges();

            _context.Set<Sensor>().Add(sensor2);
            _unitOfWork.SaveChanges(); 

            // Act
            var result = _sensorRepository.GetAll();

            // Assert
            Assert.AreEqual(2, result.Count());
        }

        /// <summary>Prueba para verificar que Update modifica un sensor.</summary>
        [TestMethod]
        public void Update_ShouldModifySensor()
        {
            // Arrange
            var someMagnitude = new PhysicalMagnitude("Temperature", "Celsius");
            var sensor = new Sensor(Guid.NewGuid(), "SENSOR005", someMagnitude, "ManufacturerE", CommunicationProtocol.UA, "PrincipleE");

            _context.Set<Sensor>().Add(sensor);
            _unitOfWork.SaveChanges(); 

            // Act
            sensor.Manufacturer = "UpdatedManufacturer";

            _sensorRepository.Update(sensor);
            _unitOfWork.SaveChanges(); 

            // Assert
            var result = _context.Set<Sensor>().Find(sensor.Id);
            Assert.AreEqual("UpdatedManufacturer", result.Manufacturer);
        }

        /// <summary>Prueba para verificar que Delete elimina un sensor.</summary>
        [TestMethod]
        public void Delete_ShouldRemoveSensor()
        {
            // Arrange
            var someMagnitude = new PhysicalMagnitude("Temperature", "Celsius");
            var sensor = new Sensor(Guid.NewGuid(), "SENSOR006", someMagnitude, "ManufacturerF", CommunicationProtocol.UA, "PrincipleF");

            _context.Set<Sensor>().Add(sensor);
            _unitOfWork.SaveChanges(); 

            // Act
            _sensorRepository.Delete(sensor.Id);
            _unitOfWork.SaveChanges(); 

            // Assert
            var result = _context.Set<Sensor>().Find(sensor.Id);
            Assert.IsNull(result);
        }

        /// <summary>Prueba para verificar que Add agrega un actuador.</summary>
        [TestMethod]
        public void Add_ShouldAddActuador()
        {
            // Arrange
            var someMagnitude = new PhysicalMagnitude("Temperature", "Celsius");
            var actuador = new Actuador(Guid.NewGuid(), "ACTUADOR001", someMagnitude, "ManufacturerA", "ControlCode", SignalControl.Analog);

            // Act
            _actuatorRepository.Add(actuador);
            _unitOfWork.SaveChanges(); 

            // Assert
            var result = _context.Set<Actuador>().FirstOrDefault(a => a.AlphanumericCode == "ACTUADOR001");
            Assert.IsNotNull(result);
            Assert.AreEqual("ManufacturerA", result.Manufacturer);
        }

        /// <summary>Prueba para verificar que GetById devuelve un actuador.</summary>
        [TestMethod]
        public void GetById_ShouldReturnActuador()
        {
            // Arrange
            var someMagnitude = new PhysicalMagnitude("Temperature", "Celsius");
            var actuador = new Actuador(Guid.NewGuid(), "ACTUADOR002", someMagnitude, "ManufacturerB", "ControlCode", SignalControl.Analog);

            _context.Set<Actuador>().Add(actuador);
            _unitOfWork.SaveChanges(); 

            // Act
            var result = _actuatorRepository.GetById(actuador.Id);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("ACTUADOR002", result.AlphanumericCode);
        }

        /// <summary>Prueba para verificar que GetAll devuelve todos los actuadores.</summary>
        [TestMethod]
        public void GetAll_ShouldReturnAllActuadores()
        {
            // Arrange
            var someMagnitude = new PhysicalMagnitude("Temperature", "Celsius");
            var actuador1 = new Actuador(Guid.NewGuid(), "ACTUADOR003", someMagnitude, "ManufacturerC", "ControlCode", SignalControl.Analog);
            var SomeMagnitude = new PhysicalMagnitude("Temperature", "Celsius");
            var actuador2 = new Actuador(Guid.NewGuid(), "ACTUADOR004", SomeMagnitude, "ManufacturerD", "ControlCode", SignalControl.Analog);

            _context.Set<Actuador>().AddRange(actuador1, actuador2);
            _unitOfWork.SaveChanges(); 

            // Act
            var result = _actuatorRepository.GetAll();

            // Assert
            Assert.AreEqual(2, result.Count());
        }

        /// <summary>Prueba para verificar que Update modifica un actuador.</summary>
        [TestMethod]
        public void Update_ShouldModifyActuador()
        {
            // Arrange
            var someMagnitude = new PhysicalMagnitude("Temperature", "Celsius");
            var actuador = new Actuador(Guid.NewGuid(), "ACTUADOR005", someMagnitude, "ManufacturerE", "ControlCode", SignalControl.Analog);

            _context.Set<Actuador>().Add(actuador);
            _unitOfWork.SaveChanges(); 

            // Act
            actuador.Manufacturer = "UpdatedManufacturer";

            _actuatorRepository.Update(actuador);
            _unitOfWork.SaveChanges(); 

            // Assert
            var result = _context.Set<Actuador>().Find(actuador.Id);
            Assert.AreEqual("UpdatedManufacturer", result.Manufacturer);
        }

        /// <summary>Prueba para verificar que Delete elimina un actuador.</summary>
        [TestMethod]
        public void Delete_ShouldRemoveActuador()
        {
            // Arrange
            var someMagnitude = new PhysicalMagnitude("Temperature", "Celsius");
            var actuador = new Actuador(Guid.NewGuid(), "ACTUADOR006", someMagnitude, "ManufacturerF", "ControlCode", SignalControl.Analog);

            _context.Set<Actuador>().Add(actuador);
            _unitOfWork.SaveChanges();

            // Act
            _actuatorRepository.Delete(actuador.Id);
            _unitOfWork.SaveChanges(); 

            // Assert
            var result = _context.Set<Actuador>().Find(actuador.Id);
            Assert.IsNull(result);
        }
    }
}
