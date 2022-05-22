using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToroBank.Application.Common.Identity.Models;
using ToroBank.Application.Features.Authentication;
using ToroBank.Domain.Entities;
using ToroBank.Infrastructure.Persistence.Context;

namespace ToroBank.Infrastructure.Persistence.Repositories
{
    public class AuthRepository : GenericRepository<User, int>, IAuthRepository
    {
        private readonly DbSet<User> _users;
        public AuthRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _users = dbContext.Set<User>();
        }

        public Task<Token?> GenerateToken()
        {
            throw new NotImplementedException();
        }

        public Task<int?> GetIdByToken(string token)
        {
            throw new NotImplementedException();
        }

        public Task<User?> ValidateCredentialsAsync(string email, string password)
        {
            throw new NotImplementedException();
        }
    }
}
