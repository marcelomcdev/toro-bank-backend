using MediatR;
using ToroBank.Application.Common.Wrappers;

namespace ToroBank.Application.Features.PurchaseOrder.Commands
{
    public class SubmitPurchaseOrderCommand : IRequest<Result<string>>
    {
        public int UserId { get; set; }
        public string? Symbol { get; set; }
        public int Amount { get; set; }
    }
}
