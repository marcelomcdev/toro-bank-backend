using FluentValidation;
using ToroBank.Application.Common.Exceptions.Constants;

namespace ToroBank.Application.Features.PurchaseOrder.Commands
{
    public class SubmitPurchaseOrderValidator : AbstractValidator<SubmitPurchaseOrderCommand>
    {
        public SubmitPurchaseOrderValidator()
        {
            string errorPropertyNull = ErrorMessage.ErrorPropertyNull;
            string errorPropertyEmpty = ErrorMessage.ErrorPropertyEmpty;

            RuleFor(p => p).NotEmpty().WithMessage(errorPropertyEmpty).NotNull().WithMessage(errorPropertyNull);
            RuleFor(p => p.UserId).NotEmpty().WithMessage(errorPropertyEmpty).NotNull().WithMessage(errorPropertyNull);
            RuleFor(p => p.Symbol).NotEmpty().WithMessage(errorPropertyEmpty).NotNull().WithMessage(errorPropertyNull);
            RuleFor(p => p.Amount).NotEmpty().WithMessage(errorPropertyEmpty).NotNull().WithMessage(errorPropertyNull).NotEqual(0).WithMessage("A quantidade não pode ser zero.");
        }
    }
}
