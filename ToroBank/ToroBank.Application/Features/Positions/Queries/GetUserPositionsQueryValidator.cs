using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToroBank.Application.Features.Positions.Queries
{
    

    public class GetUserPositionsQueryValidator : AbstractValidator<GetUserPositionQuery>
    {
        public GetUserPositionsQueryValidator()
        {
            RuleFor(f => f.PageNumber).GreaterThan(0).WithMessage("{PropertyName} deve ser maior que zero.");
            RuleFor(f => f.PageSize).GreaterThan(0).WithMessage("{PropertyName} deve ser maior que zero.");
        }

    }
}
