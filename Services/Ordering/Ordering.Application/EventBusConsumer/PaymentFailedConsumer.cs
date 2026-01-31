using EventBus.Messages.Events;
using MassTransit;
using Microsoft.Extensions.Logging;
using Ordering.Core.Repositories;

namespace Ordering.Application.EventBusConsumer
{
    public class PaymentFailedConsumer : IConsumer<PaymentFailedEvent>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ILogger<PaymentFailedConsumer> _logger;

        public PaymentFailedConsumer(IOrderRepository orderRepository, ILogger<PaymentFailedConsumer> logger)
        {
            _orderRepository = orderRepository;
            _logger = logger;
        }
        public async Task Consume(ConsumeContext<PaymentFailedEvent> context)
        {
            var order = await _orderRepository.GetByIdAsync(context.Message.OrderId);
            if (order == null)
            {
                _logger.LogWarning("Order not found for Id: {OrderId} and {CoorelationId}", context.Message.OrderId, context.Message.CorrelationId);
                return;
            }
            order.Status = Core.Entities.OrderStatus.Failed;
            await _orderRepository.UpdateAsync(order);
            _logger.LogWarning("Payment failed for Order Id: {OrderId}, Reason: {Reason}", context.Message.OrderId, context.Message.Reason);
        }
    }
}
