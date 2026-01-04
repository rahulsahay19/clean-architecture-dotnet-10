using MediatR;

namespace Basket.Application.Commands
{
    public record DeleteBasketByUserNameCommand(string userName): IRequest<Unit>;
}
