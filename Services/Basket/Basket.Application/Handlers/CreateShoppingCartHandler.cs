using Basket.Application.Commands;
using Basket.Application.Mappers;
using Basket.Application.Responses;
using Basket.Core.Repositories;
using MediatR;

namespace Basket.Application.Handlers
{
    public class CreateShoppingCartHandler : IRequestHandler<CreateShoppingCartCommand, ShoppingCartResponse>
    {
        private readonly IBasketRepository _basketRepository;

        public CreateShoppingCartHandler(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }
        public async Task<ShoppingCartResponse> Handle(CreateShoppingCartCommand request, CancellationToken cancellationToken)
        {
            // Convert Command to Domain Entity
            var shoppingCartEntity = request.ToEntity();
            // Save to Redis
            var updatedCart = await _basketRepository.UpsertBasket(shoppingCartEntity);
            // Convert back to Response
            return updatedCart.ToResponse();
        }
    }
}
