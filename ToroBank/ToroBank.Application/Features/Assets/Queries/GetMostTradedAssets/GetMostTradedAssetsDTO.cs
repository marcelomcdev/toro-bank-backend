using ToroBank.Domain.Entities;

namespace ToroBank.Application.Features.Assets.Queries.GetMostTradedAssets
{
    public record GetMostTradedAssetsDTO(
        Asset Asset,
        int Quantity
        )
    {
        public static GetMostTradedAssetsDTO ToDTO(NegotiatedAsset mna)
        {
            return new GetMostTradedAssetsDTO(mna.Asset, mna.Quantity);
        }
    }
}
