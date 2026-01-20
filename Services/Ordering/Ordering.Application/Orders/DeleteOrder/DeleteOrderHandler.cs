using Microsoft.Extensions.Logging;
using Ordering.Application.Abstractions;
using Ordering.Application.Exceptions;
using Ordering.Core.Entities;
using Ordering.Core.Repositories;

namespace Ordering.Application.Orders.DeleteOrder
{
    public class DeleteOrderHandler : ICommandHandler<DeleteOrderCommand>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ILogger<DeleteOrderHandler> _logger;

        public DeleteOrderHandler(IOrderRepository orderRepository, ILogger<DeleteOrderHandler> logger)
        {
            _orderRepository = orderRepository;
            _logger = logger;
        }
        public async Task Handle(DeleteOrderCommand command, CancellationToken cancellationToken)
        {
            var orderToDelete = await _orderRepository.GetByIdAsync(command.Id);
            if (orderToDelete == null)
            { 
                throw new OrderNotFoundException(nameof(Order), command.Id);
            }

            await _orderRepository.DeleteAsync(orderToDelete);
            _logger.LogInformation($"Order with ID {command.Id} has been deleted successfully");
        }
    }
}
