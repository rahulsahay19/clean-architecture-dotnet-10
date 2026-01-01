using MediatR;

namespace Catalog.Application.Commands
{
    public record DeleteProductByIdCommand(string Id): IRequest<bool>;
}
