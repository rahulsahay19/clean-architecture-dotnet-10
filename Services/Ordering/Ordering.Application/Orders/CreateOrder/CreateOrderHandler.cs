using Microsoft.Extensions.Logging;
using Ordering.Application.Abstractions;
using Ordering.Application.Mapper;
using Ordering.Core.Repositories;

namespace Ordering.Application.Orders.CreateOrder
{
    public class CreateOrderHandler : ICommandHandler<CreateOrderCommand, int>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ILogger<CreateOrderHandler> _logger;

        public CreateOrderHandler(IOrderRepository orderRepository, ILogger<CreateOrderHandler> logger)
        {
            _orderRepository = orderRepository;
            _logger = logger;
        }

        public async Task<int> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
        {
            var orderEntity = command.ToEntity();
            var generatedOrder = await _orderRepository.AddAsync(orderEntity);
            var outboxMessage = OrderMapper.ToOutboxMessage(generatedOrder, command.CorrelationId);
            await _orderRepository.AddOutboxMessageAsync(outboxMessage);
            _logger.LogInformation($"Order with ID {generatedOrder.Id} successfully created.");
            return generatedOrder.Id;
        }
    }
}
