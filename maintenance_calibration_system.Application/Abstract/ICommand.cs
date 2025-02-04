using MediatR;


namespace maintenance_calibration_system.Application.Abstract
{
    public interface ICommand : IRequest
    {

    }

    public interface ICommand<TResponse> : IRequest<TResponse>
    {

    }
}
