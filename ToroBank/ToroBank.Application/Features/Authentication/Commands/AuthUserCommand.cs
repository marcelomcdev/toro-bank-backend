using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToroBank.Application.Common.Wrappers;

namespace ToroBank.Application.Features.Authentication.Commands
{
    public class AuthUserCommand : IRequest<Result<string>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}


