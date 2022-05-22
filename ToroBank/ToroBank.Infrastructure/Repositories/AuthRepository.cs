using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
            //var user = await _userRepository.GetByEmailAsync(email);

            //var tokenHandler = new JwtSecurityTokenHandler();
            //var key = Encoding.ASCII.GetBytes(_generalConfig.Secret);

            //var tokenDescriptor = new SecurityTokenDescriptor
            //{
            //    Issuer = _generalConfig.Issuer,
            //    Audience = _generalConfig.ValidIn.First(),
            //    Expires = DateTime.UtcNow.AddHours(_generalConfig.ExpirationHours),
            //    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            //};
            //var token = tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
            ////Response.Headers.Add("access-token", token);
            ////Response.Headers.Add("client", user.UserName);
            ////Response.Headers.Add("uid", user.Id);

            //return new Token()
            //{
            //    AccessToken = token,
            //    Client = user?.Name,
            //    UID = $"{user.Id}"
            //};
            throw new NotImplementedException();

        }

        public Task<User?> ValidateCredentialsAsync(string email, string password)
        {
            throw new NotImplementedException();
        }
    }
}
