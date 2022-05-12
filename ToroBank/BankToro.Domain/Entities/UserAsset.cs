using ToroBank.Domain.Common;

namespace ToroBank.Domain.Entities
{
    public class UserAsset : BaseAuditableEntity<int>
    {
        protected UserAsset() { }
        public UserAsset(int assetId, int userId, int quantity)
        {
            AssetId = assetId;
            UserId = userId;
            Quantity = quantity;
        }
        public override int Id { get; set; }
        public int AssetId { get; set; }
        public int UserId { get; set; }
        public int Quantity { get; set; }

    }
}
