using Basket.Application.Commands;
using Basket.Application.Mappers;
using Basket.Application.Queries;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Basket.Application.Handlers
{
    public class BasketCheckoutHandler : IRequestHandler<BasketCheckoutCommand, Unit>
    {
        private readonly IMediator _mediator;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly ILogger<BasketCheckoutHandler> _logger;

        public BasketCheckoutHandler(IMediator mediator, IPublishEndpoint publishEndpoint, ILogger<BasketCheckoutHandler> logger)
        {
            _mediator = mediator;
            _publishEndpoint = publishEndpoint;
            _logger = logger;
        }

        public async Task<Unit> Handle(BasketCheckoutCommand request, CancellationToken cancellationToken)
        {
            var basketDto = request.BasketCheckoutDto;
            var basketResponse = await _mediator.Send(new GetBasketByUserNameQuery(basketDto.UserName), cancellationToken);
            if (basketResponse is null || !basketResponse.Items.Any())
            {
                throw new InvalidOperationException("Basket not found or empty");
            }
            var basket = basketResponse.ToEntity();
            //Map
            var evt = basketDto.ToBasketCheckoutEvent(basket);
            _logger.LogInformation("Publishing BasketCheckoutEvent for {User}", basket.UserName);
            await _publishEndpoint.Publish(evt, cancellationToken);
            //delete the basket
            await _mediator.Send(new DeleteBasketByUserNameCommand(basketDto.UserName), cancellationToken);
            return Unit.Value;
        }
    }
}
