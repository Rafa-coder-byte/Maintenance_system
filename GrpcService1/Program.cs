using GrpcService1.Services;
using maintenance_calibration_system.Contacts;
using maintenance_calibration_system.Contracts;
using maintenance_calibration_system.DataAccess.Contexts;
using maintenance_calibration_system.DataAccess.Respositories.Equipments;
using maintenance_calibration_system.DataAccess.Respositories.MaintenanceActivitiy;
using maintenance_calibration_system.DataAccess.Respositories.Plannings;
using maintenance_calibration_system.Application;
using maintenance_calibration_system.Application.Equipments.Commands.UpdateActuador;
using maintenance_calibration_system.Application.Equipments.Commands.UpdateSensor;
using maintenance_calibration_system.Application.MaintenanceActivity.Command.UpdateCalibration;
using maintenance_calibration_system.Application.Plannings.Commands.UpdatePlanning;
using MediatR;
using Microsoft.Extensions.Logging;



namespace GrpcService1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddSingleton("Data Source=maintenance_calibration_systemDB.sqlite");
            builder.Services.AddScoped<ApplicationContext>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped(typeof(IEquipmentRepository<>),typeof(EquipmentRepository<>));
            builder.Services.AddScoped(typeof(IMaintenanceActivityRepository<>), typeof(MaintenanceActivityRepository<>));
            builder.Services.AddScoped<IPlanningRepository, PlanningRepository>();
            builder.Services.AddGrpc();
            builder.Services.AddAutoMapper(typeof(Program).Assembly);
            builder.Services.AddMediatR(new MediatRServiceConfiguration()
            {
                AutoRegisterRequestProcessors = true,
            }
            .RegisterServicesFromAssemblies(typeof(AssemblyReference).Assembly));
            // Agregar servicios al contenedor
            builder.Services.AddLogging();
            builder.Logging.AddConsole(); // Esto permite que los logs se muestren en la consola
            builder.Logging.AddDebug(); // Esto permite que los logs se muestren en la ventana de salida de Visual Studio

            builder.Services.AddGrpc(options =>
            {
                options.EnableDetailedErrors = true; // Esto habilita los errores detallados
            });

                // Asegurar que ILogger<T> está registrado
                builder.Services.AddTransient(typeof(ILogger<>), typeof(Logger<>));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.MapGrpcService<SensorsService>();
            app.MapGrpcService<ActuadoresService>();
            app.MapGrpcService<PlanningsService>();
            app.MapGrpcService<CalibrationsService>();
            app.MapGrpcService<MaintenancesService>();

            app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

            app.Run();
        }
    }
}