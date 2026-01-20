using Ordering.Application.Abstractions;

namespace Ordering.Application.Orders.DeleteOrder
{
    public record DeleteOrderCommand : ICommand
    {
        public int Id { get; init; }
    }
}
