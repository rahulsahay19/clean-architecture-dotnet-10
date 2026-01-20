using Microsoft.Extensions.Logging;
using Ordering.Application.Abstractions;
using Ordering.Application.DTOs;
using Ordering.Application.Mapper;
using Ordering.Core.Repositories;

namespace Ordering.Application.Orders.GetOrders
{
    public class GetOrderListHandler : IQueryHandler<GetOrderListQuery, List<OrderDto>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ILogger<GetOrderListHandler> _logger;

        public GetOrderListHandler(IOrderRepository orderRepository, ILogger<GetOrderListHandler> logger)
        {
            _orderRepository = orderRepository;
            _logger = logger;
        }
        public async Task<List<OrderDto>> Handle(GetOrderListQuery query, CancellationToken cancellationToken)
        {
            var orders = await _orderRepository.GetOrdersByUserName(query.UserName);
            _logger.LogInformation("Successfully fetched the order.");
            return orders.Select(o => o.ToDto()).ToList();
        }
    }
}
