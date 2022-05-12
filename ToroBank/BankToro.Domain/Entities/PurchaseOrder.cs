using ToroBank.Domain.Common;

namespace ToroBank.Domain.Entities
{
    public class PurchaseOrder : BaseAuditableEntity<int>
    {
        public PurchaseOrder()
        {
            AcquiredAssets = new List<UserAsset>();
        }
        public override int Id { get; set; }
        public User User { get; set; }
        public List<UserAsset> AcquiredAssets { get; set; }
        
    }
}
