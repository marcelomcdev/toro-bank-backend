using Microsoft.EntityFrameworkCore;
using ToroBank.Application.Features.UserAssets;
using ToroBank.Domain.Entities;
using ToroBank.Infrastructure.Persistence.Context;

namespace ToroBank.Infrastructure.Persistence.Repositories
{
    public class UserAssetRepository : GenericRepository<UserAsset, int>, IUserAssetRepository
    {
        private readonly DbSet<UserAsset> _userAssets;
        public UserAssetRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _userAssets = dbContext.Set<UserAsset>();
        }

        public async Task<List<UserAsset>> GetAssetsByUserAsync(int userId)
        {
            return await _userAssets.Where(f => f.UserId == userId).ToListAsync();
        }

       
    }

}
