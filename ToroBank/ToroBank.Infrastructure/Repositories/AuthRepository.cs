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
    public class AuthRepository : GenericRepository<User, int>, IAuthOperationRepository
    {
        private readonly DbSet<User> _users;
        public AuthRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _users = dbContext.Set<User>();
        }
    }
}
