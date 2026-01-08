using Discount.Application.Commands;
using Discount.Application.Extensions;
using Discount.Core.Repositories;
using MediatR;

namespace Discount.Application.Handlers
{
    public class DeleteDiscountHandler : IRequestHandler<DeleteDiscountCommand, bool>
    {
        private readonly IDiscountRepository _discountRepository;

        public DeleteDiscountHandler(IDiscountRepository discountRepository)
        {
            _discountRepository = discountRepository;
        }
        public async Task<bool> Handle(DeleteDiscountCommand request, CancellationToken cancellationToken)
        {
            //Validate the input
            if (string.IsNullOrWhiteSpace(request.ProductName))
            {
                var validationErrors = new Dictionary<string, string>()
                {
                    {"ProductName", "Product name must not be empty." }
                };
                throw GrpcErrorHelper.CreateValidationException(validationErrors);
            }
            var deleted = await _discountRepository.DeleteDiscount(request.ProductName);
            return deleted;
        }
    }
}
