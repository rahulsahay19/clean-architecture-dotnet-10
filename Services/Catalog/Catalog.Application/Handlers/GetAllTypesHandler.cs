using Catalog.Application.Mappers;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.Repositories;
using MediatR;

namespace Catalog.Application.Handlers
{
    public class GetAllTypesHandler : IRequestHandler<GetAllTypesQuery, IList<TypesResponse>>
    {
        private readonly ITypeRepository _typeRepository;

        public GetAllTypesHandler(ITypeRepository typeRepository)
        {
            _typeRepository = typeRepository;
        }
        public async Task<IList<TypesResponse>> Handle(GetAllTypesQuery request, CancellationToken cancellationToken)
        {
            var typesList = await _typeRepository.GetAllTypes();
            return typesList.ToResponseList();
        }
    }
}
