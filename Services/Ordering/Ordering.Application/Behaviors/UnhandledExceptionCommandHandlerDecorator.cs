using Microsoft.Extensions.Logging;
using Ordering.Application.Abstractions;

namespace Ordering.Application.Behaviors
{
    public class UnhandledExceptionCommandHandlerDecorator<TCommand, TResult>
        : ICommandHandler<TCommand, TResult> where TCommand : ICommand<TResult>
    {
        private readonly ICommandHandler<TCommand, TResult> _inner;
        private readonly ILogger<TCommand> _logger;

        public UnhandledExceptionCommandHandlerDecorator(
            ICommandHandler<TCommand, TResult> inner,
            ILogger<TCommand> logger)
        {
            _inner = inner;
            _logger = logger;
        }
        public async Task<TResult> Handle(TCommand command, CancellationToken cancellationToken)
        {
            try
            {
                return await _inner.Handle(command, cancellationToken);
            }
            catch (Exception ex) 
            {
                var commandName = typeof(TCommand).Name;
                _logger.LogError(ex, "Unhandled exception occurred {CommandName}. Command:{@Command}", commandName, command);
                throw;
            }
        }
    }
}
