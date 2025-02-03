using maintenance_calibration_system.Application.Abstract;
using maintenance_calibration_system.Application.Equipments.Queries.GetActuador;
using maintenance_calibration_system.Contacts;
using maintenance_calibration_system.Contracts;
using maintenance_calibration_system.Domain.Datos_de_Planificación;


namespace maintenance_calibration_system.Application.Plannings.Queries.GetPlanning
{
    public class GetPlanningByIdQueryHandler(IPlanningRepository planningRepository)
                : IQueryHandler<GetPlanningByIdQuery, Planning?>
    {
        private readonly IPlanningRepository _planningRepository = planningRepository;

        public Task<Planning?> Handle(GetPlanningByIdQuery request, CancellationToken cancellationToken)
        {
            // Obtener todos los sensores del repositorio
            Planning? planning = _planningRepository.GetById(request.Id);

            return Task.FromResult(planning);
        }
    }
}
