using Basket.Application.Responses;
using MediatR;

namespace Basket.Application.Queries
{
    public record GetBasketByUserNameQuery(string UserName): IRequest<ShoppingCartResponse>;   
}
