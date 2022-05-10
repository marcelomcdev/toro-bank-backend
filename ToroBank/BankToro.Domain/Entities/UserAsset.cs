using ToroBank.Domain.Common;

namespace ToroBank.Domain.Entities
{
    public class UserAsset : BaseAuditableEntity<int>
    {
        protected UserAsset() { }
        public UserAsset(int assetId, int userId)
        {
            AssetId = assetId;
            UserId = userId;
        }
        public override int Id { get; set; }
        public int AssetId { get; set; }
        public int UserId { get; set; }
       
    }
}
