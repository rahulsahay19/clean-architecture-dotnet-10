using EventBus.Messages.Events;
using MassTransit;

namespace Payment.Consumer
{
    public class OrderCreatedConsumer : IConsumer<OrderCreatedEvent>
    {
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly ILogger<OrderCreatedConsumer> _logger;

        public OrderCreatedConsumer(IPublishEndpoint publishEndpoint, ILogger<OrderCreatedConsumer> logger)
        {
            _publishEndpoint = publishEndpoint;
            _logger = logger;
        }
        public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
        {
            var message = context.Message;
            _logger.LogInformation("Processing Payment for Order Id: {@OrderId}", message.Id);

            // Simulate for Payment Processing
            await Task.Delay(1000);
            if (message.TotalPrice > 0)
            {
                //Simulate Success
                var completedEvent = new PaymentCompletedEvent
                {
                    OrderId = message.Id,
                    CorrelationId = context.CorrelationId.Value
                };
                await _publishEndpoint.Publish(completedEvent);
                _logger.LogInformation("Payment success for Order Id: {@Order Id} and Correlation Id: {@CorrelationId}", message.Id, message.CorrelationId);
            }
            else
            {
                var failedEvent = new PaymentFailedEvent
                {
                    OrderId = message.Id,
                    CorrelationId = context.CorrelationId.Value,
                    Reason = "Total price was zero or negative."
                };
                await _publishEndpoint.Publish(failedEvent);
                _logger.LogWarning("Payment failed for Order Id: {@Order Id} and Correlation Id: {@CorrelationId}", message.Id, message.CorrelationId);
            }
        }
    }
}
