using FluentAssertions;
using NUnit.Framework;
using ToroBank.Application.Common.Exceptions;
using ToroBank.Application.Features.Users.Queries.GetUserById;
using NUnit.Framework;
using System.Threading.Tasks;
using ToroBank.Domain.Entities;
using Moq;
using ToroBank.Application.Features.Users;
using System.Threading;
using MediatR;
using ToroBank.Application.Common.Wrappers;
using ToroBank.Application.Common.Interfaces.Repositories;
using ToroBank.Domain.Common.Configuration;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Linq;
using ToroBank.Application.Features.Authentication;

namespace Application.UnitTests.UseCases.Users.Commands
{
    public class AutenticateUserTest
    {

        [SetUp]
        public void Setup()
        {
            
        }

        [Test]
        public void Should_return_a_token_if_email_and_password_is_valid()
        {
            Assert.Fail();
        }

        [Test]
        public void Should_return_an_error_if_email_is_null()
        {
            Assert.Fail();
        }


        [Test]
        public void Should_return_an_error_if_password_is_is_null()
        {
            Assert.Fail();
        }

        [Test]
        public void Should_return_an_error_if_email_is_empty()
        {
            Assert.Fail();
        }


        [Test]
        public void Should_return_an_error_if_password_is_is_empty()
        {
            Assert.Fail();
        }

        [Test]
        public void Should_return_an_error_if_email_is_invalid()
        {
            Assert.Fail();
        }


        [Test]
        public void Should_return_an_error_if_password_is_invalid()
        {
            Assert.Fail();
        }
    }


    public class Token
    {
        public string? AccessToken { get; set; }
        public string? Client { get; set; }
        public string? UID { get; set; }
    }

    public class TokenRequest
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
    }

    public class TokenResponse
    {
        public Token? token { get; set; }
    }

    public interface ITokenService
    {
        Task<bool> IsValid(TokenRequest request);
        Task<TokenResponse?> Authenticate(TokenRequest request);
        Task<Token?> GenerateJwtToken(string email);
    }

    public class TokenService : ITokenService
    {
        private readonly Token? _token;
        private readonly GeneralConfig _generalConfig;
        //private readonly HttpContext _httpContext;

        private readonly IUserRepository _userRepository;

        public TokenService(IUserRepository userRepository, IOptions<GeneralConfig> generalConfig)
        {
            _userRepository = userRepository;
            _generalConfig = generalConfig.Value;
        }

        public async Task<TokenResponse?> Authenticate(TokenRequest request)
        {
            if (await IsValid(request))
            {
                Token jwtToken = await GenerateJwtToken(request.Email);
                return new TokenResponse() { token = jwtToken };
            }
            else
                return null;
        }

        public async Task<bool> IsValid(TokenRequest request)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email);
            if (user == null)
                return false;
            else
            {
                if (user.Username.Equals(request.Email) && user.Password.Equals(request.Password))
                    return true;
                else
                    return false;
            }
        }

        public async Task<Token> GenerateJwtToken(string email)
        {
            var user = await _userRepository.GetByEmailAsync(email);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_generalConfig.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _generalConfig.Issuer,
                Audience = _generalConfig.ValidIn.First(),
                Expires = DateTime.UtcNow.AddHours(_generalConfig.ExpirationHours),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));


            return new Token()
            {
                AccessToken = token,
                Client = user?.Name,
                UID = $"{user.Id}"
            };
        }

       


    }


    public class AuthUserCommand : IRequest<Result<Token?>>
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
    }


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
            TokenResponse? response = await _tokenService.Authenticate(new TokenRequest() { Email = request?.Email, Password = request?.Password });
            return Result<Token>.Ok(response?.token);
        }
    }

}
