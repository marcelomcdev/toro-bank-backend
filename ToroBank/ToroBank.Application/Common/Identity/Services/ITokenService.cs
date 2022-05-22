using ToroBank.Application.Common.Identity.Models;


namespace ToroBank.Application.Common.Identity.Services
{
    public interface ITokenService
    {
        Task<bool> IsValid(TokenRequest request);
        Task<TokenResponse?> Authenticate(TokenRequest request);
        Task<Token?> GenerateJwtToken(string email);
    }
}
