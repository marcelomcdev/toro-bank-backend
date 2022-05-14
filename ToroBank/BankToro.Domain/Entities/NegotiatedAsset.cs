using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToroBank.Domain.Common;

namespace ToroBank.Domain.Entities
{
    public class NegotiatedAsset : BaseEntity<Guid>
    {
        public override Guid Id { get; set; }
        public int UserId { get; set; }
        public Asset Asset { get; set; }
        public int Quantity { get; set; }
        public DateTime AcquiredAt { get; set; }
    }
}
