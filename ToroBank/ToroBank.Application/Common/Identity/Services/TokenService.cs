using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
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

            ClaimsIdentity identity = new ClaimsIdentity(
                new GenericIdentity(user.Id.ToString(), "id"),
                                                                new[] {
                                                                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                                                                        new Claim(ClaimTypes.Role, "User")}
            );

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _generalConfig.Issuer,
                Audience = _generalConfig.ValidIn.First(),
                Expires = DateTime.UtcNow.AddHours(_generalConfig.ExpirationHours),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Subject = identity,
                NotBefore = DateTime.UtcNow
            };
            var token = tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));


            return new Token()
            {
                AccessToken = token,
                Expires = tokenDescriptor.Expires,
                //UID = $"{user.Id}"
            };
        }

        public Task<int> GetIdByToken(string token)
        {
            try
            {
                var stream = token.Split(" ").LastOrDefault();
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(stream);
                var tokenS = handler.ReadToken(stream) as JwtSecurityToken;
                var unique_name = tokenS?.Claims?.FirstOrDefault(claim => claim.Type == "unique_name")?.Value;
                return Task.FromResult(Convert.ToInt32(unique_name ?? "0"));
            }
            catch (Exception ex)
            {
                return Task.FromResult(0);
            }
        }

    }
}
