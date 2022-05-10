using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToroBank.Application.Features.Transfer.Commands.ReceiveTransfer.Objects
{
    public class TargetTransferObjectCommand : BaseObjectTransferCommand
    {
        public string? Account { get; set; }
    }
}
