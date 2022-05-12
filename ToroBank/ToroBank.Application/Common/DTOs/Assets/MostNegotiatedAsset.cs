using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToroBank.Domain.Entities;

namespace ToroBank.Application.Common.DTOs.Assets
{
    public class MostNegotiatedAsset
    {
        public Asset Asset { get; set; }
        public int Quantity { get; set; }
    }
}
