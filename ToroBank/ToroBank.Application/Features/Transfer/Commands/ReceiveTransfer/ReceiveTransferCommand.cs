using MediatR;
using ToroBank.Application.Common.Wrappers;
using ToroBank.Application.Features.Transfer.Commands.ReceiveTransfer.Objects;

namespace ToroBank.Application.Features.Transfer.Commands.ReceiveTransfer
{
    public class ReceiveTransferCommand : IRequest<Result<int>>
    {
        public string? Event { get; set; }
        public TargetTransferObjectCommand? Target { get; set; }
        public OriginTransferObjectCommand? Origin { get; set; }
        public decimal Amount { get; set; }
    }
}
