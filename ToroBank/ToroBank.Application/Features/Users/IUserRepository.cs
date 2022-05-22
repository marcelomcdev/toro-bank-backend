using ToroBank.Application.Common.Interfaces.Repositories;
using ToroBank.Domain.Entities;

namespace ToroBank.Application.Features.Users
{
    public interface IUserRepository : IGenericRepository<User, int>
    {
        Task<User?> GetByCPFAsync(string cpf);
        Task<User?> GetByEmailAsync(string email);
    }
}

