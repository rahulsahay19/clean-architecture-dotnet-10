using Discount.Application.DTOs;
using MediatR;

namespace Discount.Application.Commands
{
    public record UpdateDiscountCommand(int Id, string ProductName, string Description, int Amount) : IRequest<CouponDto>;
  
}
