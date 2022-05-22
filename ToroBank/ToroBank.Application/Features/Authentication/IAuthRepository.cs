using ToroBank.Application.Common.Identity.Models;
using ToroBank.Application.Common.Interfaces.Repositories;
using ToroBank.Domain.Entities;

namespace ToroBank.Application.Features.Authentication
{
    public interface IAuthRepository : IGenericRepository<User, int>
    {
        Task<User?> ValidateCredentialsAsync(string email, string password);
        Task<Token?> GenerateToken();
    }
}
