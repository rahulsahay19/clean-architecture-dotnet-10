using Ordering.Application.Abstractions;

namespace Ordering.Application.Orders.DeleteOrder
{
    public record DeleteOrderCommand(int Id) : ICommand;

}
