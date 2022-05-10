using Microsoft.EntityFrameworkCore;
using ToroBank.Application.Features.Users;
using ToroBank.Domain.Entities;
using ToroBank.Infrastructure.Persistence.Context;

namespace ToroBank.Infrastructure.Persistence.Repositories
{

    public class UserRepository : GenericRepository<User, int>, IUserRepository
    {
        private readonly DbSet<User> _users;
        public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _users = dbContext.Set<User>();
        }

        public async Task<User?> GetByCPFAsync(string cpf)
        {
            return await _users.Where(f => f.CPF == cpf).FirstOrDefaultAsync();
        }
    }
}
