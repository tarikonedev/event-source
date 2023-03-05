namespace EventSource.Ordering.Application.Common.Commands
{
    public interface ICommandHandler<TCommand> where TCommand: class
    {
        Task Handle(TCommand command);
    }
}
