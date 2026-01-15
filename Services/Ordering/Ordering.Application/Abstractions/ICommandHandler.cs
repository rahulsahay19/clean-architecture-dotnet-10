namespace Ordering.Application.Abstractions
{
    public interface ICommandHandler<in TCommand> where TCommand: ICommand
    {
    }
}

