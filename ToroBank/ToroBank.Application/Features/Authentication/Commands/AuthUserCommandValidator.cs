using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToroBank.Application.Common.Exceptions.Constants;

namespace ToroBank.Application.Features.Authentication.Commands
{
    public class AuthUserCommandValidator : AbstractValidator<AuthUserCommand>
    {
        public AuthUserCommandValidator()
        {
            RuleFor(a => a.Email).NotEmpty().WithMessage(ErrorMessage.ErrorPropertyEmpty).NotNull().WithMessage(ErrorMessage.ErrorPropertyNull);
            RuleFor(a => a.Password).NotEmpty().WithMessage(ErrorMessage.ErrorPropertyEmpty).NotNull().WithMessage(ErrorMessage.ErrorPropertyNull);
        }
    }
}
