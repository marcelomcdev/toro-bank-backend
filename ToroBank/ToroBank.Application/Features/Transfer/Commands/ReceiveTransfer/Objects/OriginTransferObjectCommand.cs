using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToroBank.Application.Features.Transfer.Commands.ReceiveTransfer.Objects
{
    public class OriginTransferObjectCommand : BaseObjectTransferCommand
    {
        public string? CPF { get; set; }
    }
}
