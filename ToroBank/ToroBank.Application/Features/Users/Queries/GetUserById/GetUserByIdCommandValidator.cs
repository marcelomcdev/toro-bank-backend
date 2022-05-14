using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToroBank.Application.Features.Users.Queries.GetUserById
{
    public class GetUserByIdCommandValidator : AbstractValidator<GetUserByIdCommand>
    {
        public GetUserByIdCommandValidator()
        {
            RuleFor(p => p.Id).GreaterThan(0).WithMessage("{PropertyName} não pode ser zero.").NotNull();
        }
    }
}
