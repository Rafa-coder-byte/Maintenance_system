using Grpc.Core;
using Grpc.Net.Client;
using maintenance_calibration_system.GrpcProtos;

namespace maintenance_calibration_system.ConsoleApp
{
    /// <summary>Clase principal del programa que ejecuta las operaciones CRUD en la base de datos SQLite.</summary>
    internal class Program
    {
        /// <summary>Método principal del programa.</summary>
        static void Main(string[] args)
        {
            Console.WriteLine("Presione una tecla para comenzar....");
            Console.ReadKey();  

            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback =
                HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;

            var channel = GrpcChannel.ForAddress(
                "http://localhost:5032",
                new GrpcChannelOptions { HttpHandler = httpHandler });

            if (channel == null)
            {
                Console.WriteLine("Cannot connect");
                channel.Dispose();
                return;
            }

            var client = new maintenance_calibration_system.GrpcProtos.Sensor.SensorClient(channel);

            Console.WriteLine("Presione una tecla para crear un sensor");
            Console.ReadKey();

            try
            {
                var createResponse = client.CreateSensor(new GrpcProtos.CreateSensorRequest()
                {
                    AlphanumericCode = "Sensor000000",
                    Magnitude = new GrpcProtos.PhysicalMagnitude()
                    {
                        Name = "tiempo",
                        UnitofMagnitude = "segundos",
                    },
                    Manufacturer = "cualquiera",
                    Protocol = GrpcProtos.CommunicationProtocol.ModBus,
                    PrincipleOperation = "Es un contador de tiempo, regido por un pulso de reloj a una frecuencia de 100kHz",
                });
                Console.WriteLine("Sensor creado con éxito");
            }
            catch (RpcException ex)
            {
                Console.WriteLine($"Error al crear el sensor: {ex.Status.Detail}");
            }
            catch (Exception ex) 
            { 
                Console.WriteLine($"Error inesperado: {ex.Message}"); 
            }

            Console.WriteLine("Presione una tecla para salir..."); 
            Console.ReadKey();


        }
    }
}




