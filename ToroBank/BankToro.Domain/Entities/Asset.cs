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

        public Asset(int id, string? name, decimal value)
        {
            Id = id;
            Name = name;
            Value = value;
        }

        public override int Id { get; set; }
        public string? Name { get; set; }
        public decimal Value { get; set; }
    }
}
