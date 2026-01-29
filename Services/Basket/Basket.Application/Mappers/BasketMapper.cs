using Basket.Application.Commands;
using Basket.Application.DTOs;
using Basket.Application.Responses;
using Basket.Core.Entities;
using EventBus.Messages.Events;
using System.Runtime.CompilerServices;

namespace Basket.Application.Mappers
{
    public static class BasketMapper
    {
        
        public static ShoppingCart ToShoppingCartEntity(this CreateShoppingCartCommand command)
        {
            return new ShoppingCart
            {
                UserName = command.UserName,
                Items = command.Items.Select(item => new ShoppingCartItem
                {
                    Quantity = item.Quantity,
                    ImageFile = item.ImageFile,
                    Price = item.Price,
                    ProductId = item.ProductId,
                    ProductName = item.ProductName
                }).ToList()
            };
        }
        public static ShoppingCartResponse ToShoppingCartResponse(this ShoppingCart shoppingCart)
        {
            return new ShoppingCartResponse
            {
                UserName = shoppingCart.UserName,
                Items = shoppingCart.Items.Select(item => new ShoppingCartItemResponse
                {
                    Quantity = item.Quantity,
                    ImageFile = item.ImageFile,
                    Price = item.Price,
                    ProductId = item.ProductId,
                    ProductName = item.ProductName
                }).ToList()
            };
        }

        public static ShoppingCart ToEntity(this ShoppingCartResponse response)
        {
            return new ShoppingCart(response.UserName)
            {
                Items = response.Items.Select(item => new ShoppingCartItem
                {
                    ProductId = item.ProductId,
                    ProductName = item.ProductName,
                    Price = item.Price,
                    Quantity = item.Quantity
                }).ToList()
            };
        }

        public static BasketCheckoutEvent ToBasketCheckoutEvent(this BasketCheckoutDto dto, ShoppingCart basket)
        {
            return new BasketCheckoutEvent 
            {
                UserName = dto.UserName,
                TotalPrice = basket.Items.Sum(item => item.Price * item.Quantity),
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                EmailAddress = dto.EmailAddress,
                AddressLine = dto.AddressLine,
                Country = dto.Country,
                State = dto.State,
                ZipCode = dto.ZipCode,
                CardName = dto.CardName,
                CardNumber = dto.CardNumber,
                Expiration = dto.Expiration,
                Cvv = dto.Cvv,
                PaymentMethod = dto.PaymentMethod
            };
        }

        //Delegate based mapper
        public static ShoppingCartResponse ToResponseUsingDelegate(this ShoppingCart cart)
            => MapCart(cart);

        public static readonly Func<ShoppingCart, ShoppingCartResponse> MapCart =
            cart => new ShoppingCartResponse
            {
                UserName = cart.UserName,
                Items = cart.Items.Select(item => new ShoppingCartItemResponse
                {
                    Quantity = item.Quantity,
                    ImageFile = item.ImageFile,
                    Price = item.Price,
                    ProductId = item.ProductId,
                    ProductName = item.ProductName
                }).ToList()
            };
    }
}
