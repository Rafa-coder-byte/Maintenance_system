using MediatR;


namespace maintenance_calibration_system.Application.Abstract
{
    public interface ICommandHandler<TCommand>
        : IRequestHandler<TCommand>
        where TCommand : ICommand
    {

    }

    public interface ICommandHandler<TCommand, TResponse>
            : IRequestHandler<TCommand, TResponse>
            where TCommand : ICommand<TResponse>
    {

    }
}
