using AutoMapper;
using Google.Protobuf.WellKnownTypes;
using maintenance_calibration_system.Domain.Datos_Historicos;
using maintenance_calibration_system.GrpcProtos;


namespace GrpcService1.Mappers
{
    public class CalibrationProfile : Profile
    {
        public CalibrationProfile()
        {
            CreateMap<DateTime, Timestamp>()
                .ConvertUsing(dt => Timestamp.FromDateTime(dt.ToUniversalTime()));

            CreateMap<Timestamp, DateTime>()
                .ConvertUsing(ts => ts.ToDateTime());

            CreateMap<Calibration, CalibrationDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString())) // Convertir Guid a string  
                .ForMember(dest => dest.CalibratedSensors, opt => opt.MapFrom(src => new Sensors
                    {
                        Items = { src.CalibratedSensors.Select(sensor => new SensorDTO
                    {
                        Id = sensor.Id.ToString(),   // Convertir Guid a string  
                        AlphanumericCode = sensor.AlphanumericCode,
                        Magnitude = new maintenance_calibration_system.GrpcProtos.PhysicalMagnitude // Si tienes un DTO para PhysicalMagnitude  
                        {
                            Name = sensor.Magnitude.Name,
                            UnitofMagnitude = sensor.Magnitude.UnitofMagnitude
                        },
                        Manufacturer = sensor.Manufacturer,
                        Protocol = (CommunicationProtocol)sensor.Protocol, // Asumiendo que Protocol es un enum  
                        PrincipleOperation = sensor.PrincipleOperation
                    })}
                    }));

            //  .ForMember(dest => dest.CalibratedSensors, opt => opt.MapFrom(src => src.CalibratedSensors)); // Mapeo automático de la lista

            CreateMap<CalibrationDTO, Calibration>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.Parse(src.Id))) // Convertir string a Guid
                .ForMember(dest => dest.CalibratedSensors, opt => opt.MapFrom(src =>
                           src.CalibratedSensors.Items.Select(sensorDto => new maintenance_calibration_system.Domain.Datos_de_Configuracion.Sensor
                           {
                               // Mapea cada propiedad necesaria
                               Id = new Guid(sensorDto.Id),   
                               AlphanumericCode = sensorDto.AlphanumericCode,
                               Magnitude = new maintenance_calibration_system.Domain.ValueObjects.PhysicalMagnitude
                               {
                                   Name = sensorDto.Magnitude.Name,
                                   UnitofMagnitude = sensorDto.Magnitude.UnitofMagnitude
                               },
                               Manufacturer = sensorDto.Manufacturer,
                               Protocol = (maintenance_calibration_system.Domain.Types.CommunicationProtocol)sensorDto.Protocol,
                               PrincipleOperation = sensorDto.PrincipleOperation
                           }).ToList())); // Construye la lista manualmente  

        }
    }
}