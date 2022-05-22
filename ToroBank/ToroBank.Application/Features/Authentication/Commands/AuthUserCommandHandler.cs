using MediatR;
using ToroBank.Application.Common.Exceptions;
using ToroBank.Application.Common.Identity.Models;
using ToroBank.Application.Common.Identity.Services;
using ToroBank.Application.Common.Wrappers;

namespace ToroBank.Application.Features.Authentication.Commands
{
    public class AuthUserCommandHandler : IRequestHandler<AuthUserCommand, Result<Token?>>
    {
        private readonly IAuthRepository _authRepository;
        private readonly ITokenService _tokenService;
        public AuthUserCommandHandler(IAuthRepository authRepository, ITokenService tokenService)
        {
            _authRepository = authRepository;
            _tokenService = tokenService;

         }

        public async Task<Result<Token?>> Handle(AuthUserCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
                throw new NotFoundException(nameof(request));
            else if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
                throw new NotFoundException("Usuário ou senha inválidos!");
            
            TokenResponse? response = await _tokenService.Authenticate(new TokenRequest() { Email = request?.Email, Password = request?.Password });
            return Result<Token>.Ok(response?.token);
        }
    }
}
