using Catalog.Application.Responses;
using MediatR;

namespace Catalog.Application.Queries
{
    public record GetAllBrandsQuery : IRequest<IList<BrandResponse>>
    {
    }
}
