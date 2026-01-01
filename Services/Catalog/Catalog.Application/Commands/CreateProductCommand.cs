using Catalog.Application.Responses;
using MediatR;

namespace Catalog.Application.Commands
{
    public record CreateProductCommand : IRequest<ProductResponse>
    {
        public string Name { get; init; }
        public string Summary { get; init; }
        public string Description { get; init; }
        public string ImageFile { get; init; }
        public string BrandId { get; init; }
        public string TypeId { get; init; }
        public decimal Price { get; init; }
    }
}
