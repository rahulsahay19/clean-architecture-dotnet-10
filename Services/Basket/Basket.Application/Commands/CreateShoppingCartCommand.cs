using Basket.Application.DTOs;
using Basket.Application.Responses;
using MediatR;

namespace Basket.Application.Commands
{
    public record CreateShoppingCartCommand(string UserName, List<CreateShoppingCartItemDto> Items): IRequest<ShoppingCartResponse>;
}
