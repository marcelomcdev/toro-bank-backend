using FluentValidation;
using ToroBank.Application.Common.Exceptions.Constants;
using ToroBank.Application.Features.Users;

namespace ToroBank.Application.Features.Transfer.Commands.ReceiveTransfer
{
    public class ReceiveTransferCommandValidator : AbstractValidator<ReceiveTransferCommand>
    {
        private readonly IUserRepository _userRepository;
        public ReceiveTransferCommandValidator(IUserRepository _userRepository)
        {
            string errorPropertyNull = ErrorMessage.ErrorPropertyNull;
            string errorPropertyEmpty = ErrorMessage.ErrorPropertyEmpty;

            RuleFor(p => p).NotEmpty().WithMessage(errorPropertyEmpty).NotNull().WithMessage(errorPropertyNull);
            RuleFor(p => p.Event).NotEmpty().WithMessage("{PropertyName} é obrigatório.").NotNull();
            RuleFor(p => p.Event).Matches("^TRANSFER$").WithMessage("{PropertyName} não possui um valor válido.");


            RuleFor(p => p.Origin).NotEmpty().WithMessage(errorPropertyEmpty).NotNull().WithMessage(errorPropertyNull);

            RuleFor(p => p.Origin).NotNull().WithMessage(errorPropertyNull);
            RuleFor(p => p.Origin.CPF).NotEmpty().WithMessage(errorPropertyEmpty).NotNull().WithMessage(errorPropertyNull);
            RuleFor(p => p.Origin.Branch).NotEmpty().WithMessage(errorPropertyEmpty).NotNull().WithMessage(errorPropertyNull);
            RuleFor(p => p.Origin.Bank).NotEmpty().WithMessage(errorPropertyEmpty).NotNull().WithMessage(errorPropertyNull);

            RuleFor(p => p.Target).NotNull().WithMessage(errorPropertyNull);
            RuleFor(p => p.Target.Branch).NotEmpty().WithMessage(errorPropertyEmpty).NotNull().WithMessage(errorPropertyNull);
            RuleFor(p => p.Target.Bank).NotEmpty().WithMessage(errorPropertyEmpty).NotNull().WithMessage(errorPropertyNull);
            RuleFor(p => p.Target.Account).NotEmpty().WithMessage(errorPropertyEmpty).NotNull().WithMessage(errorPropertyNull);

            RuleFor(p => p.Amount).GreaterThan(0).WithMessage("O valor da transferência deve ser maior que zero.");

        }
    }
}
