using ToroBank.Domain.Common;

namespace ToroBank.Domain.Entities
{
    public class Asset : BaseAuditableEntity<int>
    {
        protected Asset() { }
        public Asset(string? name, decimal value)
        {
            Name = name;
            Value = value;
        }

        public Asset(int id, string? name, decimal value, string imageName = null)
        {
            Id = id;
            Name = name;
            Value = value;
            ImageName = imageName;
        }

        public override int Id { get; set; }
        public string? Name { get; set; }
        public decimal Value { get; set; }
        public string? ImageName { get; set; }
    }
}
