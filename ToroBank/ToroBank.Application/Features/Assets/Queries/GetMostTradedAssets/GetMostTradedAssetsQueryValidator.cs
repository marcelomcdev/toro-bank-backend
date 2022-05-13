using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToroBank.Application.Features.Assets.Queries.GetMostTradedAssets
{
    public class GetMostTradedAssetsQueryValidator : AbstractValidator<GetMostTradedAssetsQuery>
    {
        public GetMostTradedAssetsQueryValidator()
        {
            RuleFor(f=> f.PageNumber).GreaterThan(0).WithMessage("{PropertyName} deve ser maior que zero.");
            RuleFor(f => f.PageSize).GreaterThan(0).WithMessage("{PropertyName} deve ser maior que zero.");
        }
    }
}
