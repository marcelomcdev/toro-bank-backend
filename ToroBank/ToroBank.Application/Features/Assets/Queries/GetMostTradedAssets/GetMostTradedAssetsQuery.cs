using MediatR;
using ToroBank.Application.Common.Wrappers;

namespace ToroBank.Application.Features.Assets.Queries.GetMostTradedAssets
{
    public class GetMostTradedAssetsQuery : PagedRequest, IRequest<Result<PagedResponse<GetMostTradedAssetsDTO>>>
    {
    }
}
