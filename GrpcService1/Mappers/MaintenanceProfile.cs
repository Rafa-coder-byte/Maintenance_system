using AutoMapper;
using Google.Protobuf.WellKnownTypes;
using maintenance_calibration_system.Domain.Datos_Historicos;
using maintenance_calibration_system.GrpcProtos;


namespace GrpcService1.Mappers
{
    public class MaintenanceProfile : Profile
    {
        public MaintenanceProfile()
        {
            CreateMap<DateTime, Timestamp>()
                .ConvertUsing(dt => Timestamp.FromDateTime(dt.ToUniversalTime()));

            CreateMap<Timestamp, DateTime>()
                .ConvertUsing(ts => ts.ToDateTime());


            CreateMap<Maintenance, MaintenanceDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString())) // Convertir Guid a string  
                .ForMember(dest => dest.MaintenanceActuador, opt => opt.MapFrom(src => new Actuadores
                {
                    Items = { src.MaintenanceActuador.Select(actuador => new ActuadorDTO
                    {
                        Id = actuador.Id.ToString(),   // Convertir Guid a string  
                        AlphanumericCode = actuador.AlphanumericCode,
                        Magnitude = new maintenance_calibration_system.GrpcProtos.PhysicalMagnitude // Si tienes un DTO para PhysicalMagnitude  
                        {
                            Name = actuador.Magnitude.Name,
                            UnitofMagnitude = actuador.Magnitude.UnitofMagnitude
                        },
                        Manufacturer = actuador.Manufacturer,
                        CodeControl = actuador.CodeControl,
                        SignalControl = (SignalControl)actuador.SignalControl // Asumiendo que Protocol es un enum  
                        
                    })}
                }));

            //  .ForMember(dest => dest.CalibratedSensors, opt => opt.MapFrom(src => src.CalibratedSensors)); // Mapeo automático de la lista

            CreateMap<MaintenanceDTO, Maintenance>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.Parse(src.Id))) // Convertir string a Guid
                .ForMember(dest => dest.MaintenanceActuador, opt => opt.MapFrom(src =>
                    src.MaintenanceActuador.Items.Select(actuadorDto => new maintenance_calibration_system.Domain.Datos_de_Configuracion.Actuador
                    {
                        // Mapea cada propiedad necesaria
                        Id = new Guid(actuadorDto.Id),
                        AlphanumericCode = actuadorDto.AlphanumericCode,
                        Magnitude = new maintenance_calibration_system.Domain.ValueObjects.PhysicalMagnitude
                        {
                            Name = actuadorDto.Magnitude.Name,
                            UnitofMagnitude = actuadorDto.Magnitude.UnitofMagnitude
                        },
                        Manufacturer = actuadorDto.Manufacturer,
                        CodeControl = actuadorDto.CodeControl,
                        SignalControl = (maintenance_calibration_system.Domain.Types.SignalControl)actuadorDto.SignalControl
                               
                    }).ToList())); // Construye la lista manualmente  
        }
    }
}