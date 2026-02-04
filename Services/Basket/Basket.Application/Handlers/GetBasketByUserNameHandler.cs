using Basket.Application.Mappers;
using Basket.Application.Queries;
using Basket.Application.Responses;
using Basket.Core.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Basket.Application.Handlers
{
    public class GetBasketByUserNameHandler : IRequestHandler<GetBasketByUserNameQuery, ShoppingCartResponse>
    {
        private readonly IBasketRepository _basketRepository;
        private readonly ILogger<GetBasketByUserNameHandler> _logger;

        public GetBasketByUserNameHandler(IBasketRepository basketRepository, ILogger<GetBasketByUserNameHandler> logger)
        {
            _basketRepository = basketRepository;
            _logger = logger;
        }
        public async Task<ShoppingCartResponse> Handle(GetBasketByUserNameQuery request, CancellationToken cancellationToken)
        {
            var shoppingCart = await _basketRepository.GetBasket(request.UserName);
            if(shoppingCart == null) 
            {
                return new ShoppingCartResponse(request.UserName)
                {
                    Items = new List<ShoppingCartItemResponse>()
                };
            }
            _logger.LogInformation("Fetched basket for {@UserName}", request.UserName);
            return shoppingCart.ToShoppingCartResponse();
            //return BasketMapper.MapCart(shoppingCart);
        }
    }
}
