using AutoMapper;
using Google.Protobuf.WellKnownTypes;
using maintenance_calibration_system.GrpcProtos;

namespace GrpcService1.Mappers
{
    public class PlanningProfile : Profile
    {
        public PlanningProfile()
        {
            CreateMap<DateTime, Timestamp>()
                .ConvertUsing(dt => Timestamp.FromDateTime(dt.ToUniversalTime()));

            CreateMap<Timestamp, DateTime>()
                .ConvertUsing(ts => ts.ToDateTime());

            CreateMap<maintenance_calibration_system.Domain.Datos_de_Planificación.Planning,
                maintenance_calibration_system.GrpcProtos.PlanningDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString())) // Convertir Guid a string
                .ForMember(dest => dest.ExecutionDate, opt => opt.MapFrom(src => src.ExecutionDate));

            CreateMap<maintenance_calibration_system.GrpcProtos.PlanningDTO,
                maintenance_calibration_system.Domain.Datos_de_Planificación.Planning>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.Parse(src.Id))) // Convertir string a Guid
                .ForMember(dest => dest.ExecutionDate, opt => opt.MapFrom(src => src.ExecutionDate));

            // Mapeo de Planning a NullablePlanningDTO
            CreateMap<maintenance_calibration_system.Domain.Datos_de_Planificación.Planning,
                maintenance_calibration_system.GrpcProtos.NullablePlanningDTO>()
                .ForMember(dest => dest.Planning, opt => opt.MapFrom(src =>
                    new maintenance_calibration_system.GrpcProtos.PlanningDTO
                    {
                        Id = src.Id.ToString(), // Convertir Guid a string
                        ExecutionDate = Timestamp.FromDateTime(src.ExecutionDate.ToUniversalTime()) // Llama a tu método de conversión aquí
                    }));

        }

    }
}
