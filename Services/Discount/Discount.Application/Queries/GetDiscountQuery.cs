using Discount.Application.DTOs;
using MediatR;

namespace Discount.Application.Queries
{
    public record GetDiscountQuery(string ProductName): IRequest<CouponDto>;
    
}
