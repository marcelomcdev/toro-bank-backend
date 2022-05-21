using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToroBank.Domain.Common.Configuration
{
    public class GeneralConfig
    {
        public string Secret { get; set; }
        public int ExpirationHours { get; set; }
        public string Issuer { get; set; }
        public IEnumerable<string> ValidIn { get; set; }
    }
}
