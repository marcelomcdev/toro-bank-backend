using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using ToroBank.Application.Common.Identity.Models;
using ToroBank.Application.Features.Users;
using ToroBank.Domain.Common.Configuration;

namespace ToroBank.Application.Common.Identity.Services
{
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

        private async Task<Token> GenerateJwtToken(string email)
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
            //Response.Headers.Add("access-token", token);
            //Response.Headers.Add("client", user.UserName);
            //Response.Headers.Add("uid", user.Id);

            return new Token()
            {
                AccessToken = token,
                Client = user?.Name,
                UID = $"{user.Id}"
            };
        }

        Task<Token> ITokenService.GenerateJwtToken(string email)
        {
            throw new NotImplementedException();
        }
    }
}
