using MediatR;
using ToroBank.Application.Common.Identity.Models;
using ToroBank.Application.Common.Wrappers;

namespace ToroBank.Application.Features.Authentication.Commands
{
    public class AuthUserCommand : IRequest<Result<Token?>>
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}


