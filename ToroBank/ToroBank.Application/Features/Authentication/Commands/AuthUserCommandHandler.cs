using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToroBank.Application.Common.Wrappers;

namespace ToroBank.Application.Features.Authentication.Commands
{
    public class AuthUserCommandHandler : IRequestHandler<AuthUserCommand, Result<string>>
    {
        public Task<Result<string>> Handle(AuthUserCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
