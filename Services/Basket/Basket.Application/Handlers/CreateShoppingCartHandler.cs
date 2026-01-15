using Basket.Application.Commands;
using Basket.Application.GrpcService;
using Basket.Application.Mappers;
using Basket.Application.Responses;
using Basket.Core.Repositories;
using MediatR;

namespace Basket.Application.Handlers
{
    public class CreateShoppingCartHandler : IRequestHandler<CreateShoppingCartCommand, ShoppingCartResponse>
    {
        private readonly IBasketRepository _basketRepository;
        private readonly DiscountGrpcService _discountGrpcService;

        public CreateShoppingCartHandler(IBasketRepository basketRepository, DiscountGrpcService discountGrpcService)
        {
            _basketRepository = basketRepository;
            _discountGrpcService = discountGrpcService;
        }
        public async Task<ShoppingCartResponse> Handle(CreateShoppingCartCommand request, CancellationToken cancellationToken)
        {
            //Apply discounts using GRPC call
            foreach (var item in request.Items)
            {
                var coupon = await _discountGrpcService.GetDiscount(item.ProductName);
                item.Price -= coupon.Amount;
            }
            // Convert Command to Domain Entity
            var shoppingCartEntity = request.ToShoppingCartEntity();
            // Save to Redis
            var updatedCart = await _basketRepository.UpsertBasket(shoppingCartEntity);
            // Convert back to Response
            // return updatedCart.ToShoppingCartResponse();
            //return updatedCart.ToResponseUsingDelegate();
            return BasketMapper.MapCart(updatedCart);
        }
    }
}

