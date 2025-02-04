using MediatR;


namespace maintenance_calibration_system.Application.Abstract
{
    public interface IQuery<TResponse> : IRequest<TResponse>
    {

    }
}
