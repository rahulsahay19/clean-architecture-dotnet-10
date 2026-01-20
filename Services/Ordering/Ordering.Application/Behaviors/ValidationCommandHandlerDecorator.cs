using FluentValidation;
using Ordering.Application.Abstractions;
using ValidationException = FluentValidation.ValidationException;

namespace Ordering.Application.Behaviors
{
    public class ValidationCommandHandlerDecorator<TCommand, TResult> : ICommandHandler<TCommand, TResult>
        where TCommand : ICommand<TResult>
    {
        private readonly ICommandHandler<TCommand, TResult> _inner;
        private readonly IEnumerable<IValidator<TCommand>> _validators;

        public ValidationCommandHandlerDecorator(
            ICommandHandler<TCommand, TResult> inner,
            IEnumerable<IValidator<TCommand>> validators
            )
        {
            _inner = inner;
            _validators = validators;
        }
        public async Task<TResult> Handle(TCommand command, CancellationToken cancellationToken)
        {
            if (_validators.Any()) 
            {
                var context = new ValidationContext<TCommand>(command);
                var results = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));

                var failures = results.
                                SelectMany(r => r.Errors)
                                .Where(f => f != null)
                                .ToList();
                if (failures.Any())
                {
                    throw new ValidationException(failures);
                }
            }
            return await _inner.Handle(command, cancellationToken);
        }
    }
}
