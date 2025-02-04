using MediatR;


namespace maintenance_calibration_system.Application.Abstract
{
    public interface IQueryHandler<TQuery, TResponse>
        : IRequestHandler<TQuery, TResponse>
        where TQuery : IRequest<TResponse>
    {

    }
}
