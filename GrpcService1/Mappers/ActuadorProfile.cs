using AutoMapper;
using Google.Protobuf.Collections;
using Google.Protobuf.WellKnownTypes;
using maintenance_calibration_system.GrpcProtos;

namespace GrpcService1.Mappers
{
    public class ActuadorProfile : Profile
    {
        public ActuadorProfile()
        {

            CreateMap(typeof(RepeatedField<>), typeof(List<>))
                .ConvertUsing(typeof(RepeatedFieldToListTypeConverter<,>));

            CreateMap<maintenance_calibration_system.Domain.Datos_de_Configuracion.Actuador,
             maintenance_calibration_system.GrpcProtos.ActuadorDTO>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString())) // Convertir Guid a string
            .ForMember(dest => dest.Magnitude, opt => opt.MapFrom(src => new maintenance_calibration_system.GrpcProtos.PhysicalMagnitude()
            {
                Name = src.Magnitude.Name,
                UnitofMagnitude = src.Magnitude.UnitofMagnitude,
            }));

            CreateMap<maintenance_calibration_system.GrpcProtos.ActuadorDTO,
               maintenance_calibration_system.Domain.Datos_de_Configuracion.Actuador>()
              .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.Parse(src.Id))) // Convertir string a Guid
              .ForMember(dest => dest.Magnitude, opt => opt.MapFrom(src => new maintenance_calibration_system.Domain.ValueObjects.PhysicalMagnitude()
              {
                  Name = src.Magnitude.Name,
                  UnitofMagnitude = src.Magnitude.UnitofMagnitude,
              }));


            // Mapeo de Actuador a NullableActuadorDTO
            CreateMap<maintenance_calibration_system.Domain.Datos_de_Configuracion.Actuador,
                maintenance_calibration_system.GrpcProtos.NullableActuadorDTO>()
                .ForMember(dest => dest.Actuador, opt => opt.MapFrom(src => src != null ? new ActuadorDTO
                {
                    Id = src.Id.ToString(), // Convertir Guid a string
                    Magnitude = new maintenance_calibration_system.GrpcProtos.PhysicalMagnitude
                    {
                        Name = src.Magnitude.Name,
                        UnitofMagnitude = src.Magnitude.UnitofMagnitude
                    }

                } : null));

            // Mapeo específico de CalibratedSensors
            CreateMap<maintenance_calibration_system.GrpcProtos.Actuadores, List<maintenance_calibration_system.Domain.Datos_de_Configuracion.Actuador>>()
                .ConvertUsing((src, dest, ctx) =>
                {
                    if (src.Items != null)
                    {
                        return ctx.Mapper.Map<List<maintenance_calibration_system.Domain.Datos_de_Configuracion.Actuador>>(src.Items);
                    }
                    return new List<maintenance_calibration_system.Domain.Datos_de_Configuracion.Actuador>();
                });
        }


    }
}
