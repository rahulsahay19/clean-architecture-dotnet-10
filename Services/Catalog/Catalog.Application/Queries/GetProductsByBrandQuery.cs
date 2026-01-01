using Catalog.Application.Responses;
using MediatR;

namespace Catalog.Application.Queries
{
    public record GetProductsByBrandQuery(string BrandName) : IRequest<IList<ProductResponse>>;
}
