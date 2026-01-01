using Catalog.Application.Responses;
using MediatR;

namespace Catalog.Application.Queries
{
    public record GetProductByNameQuery(string name): IRequest<IList<ProductResponse>>;
    
}
