using MediatR;
using ToroBank.Application.Common.Wrappers;

namespace ToroBank.Application.Features.Assets.Queries.GetMostTradedAssets
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetMostTradedAssetsQuery, Result<PagedResponse<GetMostTradedAssetsDTO>>>
    {
        private readonly IAssetRepository _assetRepository;

        public GetAllProductsQueryHandler(IAssetRepository assetRepository)
        {
            _assetRepository = assetRepository;
        }

        public async Task<Result<PagedResponse<GetMostTradedAssetsDTO>>> Handle(GetMostTradedAssetsQuery request, CancellationToken cancellationToken)
        {
            var pagedResponse = await _assetRepository.GetMostNegotiatedAsset(request.PageNumber, request.PageSize);
            PagedResponse<GetMostTradedAssetsDTO> response = new PagedResponse<GetMostTradedAssetsDTO>(
                pagedResponse.PageNumber, 
                pagedResponse.PageSize, 
                pagedResponse.TotalCount, 
                pagedResponse.Data.Select(f=> GetMostTradedAssetsDTO.ToDTO(f)).ToList().AsReadOnly());
            return Result.Ok(response);
        }
    }
}
