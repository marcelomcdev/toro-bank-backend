using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToroBank.Application.Common.Exceptions.Constants;

namespace ToroBank.Application.Features.Authentication.Queries
{
    public class GetIdByTokenQueryValidator : AbstractValidator<GetIdByTokenQuery>
    {
        public GetIdByTokenQueryValidator()
        {
            RuleFor(p => p.Token).NotNull().WithMessage(ErrorMessage.ErrorPropertyNull).NotEmpty().WithMessage(ErrorMessage.ErrorPropertyEmpty);
        }
    }
}
