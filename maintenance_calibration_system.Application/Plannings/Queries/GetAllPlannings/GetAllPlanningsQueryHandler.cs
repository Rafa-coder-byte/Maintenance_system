using maintenance_calibration_system.Application.Abstract;
using maintenance_calibration_system.Application.Equipments.Queries.GetAllActuador;
using maintenance_calibration_system.Contacts;
using maintenance_calibration_system.Contracts;
using maintenance_calibration_system.DataAccess.Respositories.Plannings;
using maintenance_calibration_system.Domain.Datos_de_Planificación;


namespace maintenance_calibration_system.Application.Plannings.Queries.GetAllPlannings
{
    public class GetAllPlanningQueryHandler(IPlanningRepository planningRepository)
                : IQueryHandler<GetAllPlanningQuery, List<Planning>>
    {
        private readonly IPlanningRepository _planningRepository = planningRepository;

        public Task<List<Planning>> Handle(GetAllPlanningQuery request, CancellationToken cancellationToken)
        {
            // Obtener todos los sensores del repositorio
            var plannings = _planningRepository.GetAll();

            return Task.FromResult(plannings.ToList());
        }
    }
}
