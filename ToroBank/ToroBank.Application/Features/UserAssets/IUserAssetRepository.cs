using ToroBank.Application.Common.Interfaces.Repositories;
using ToroBank.Domain.Entities;

namespace ToroBank.Application.Features.UserAssets
{

    public interface IUserAssetRepository : IGenericRepository<UserAsset, int>
    {
        Task<List<UserAsset>> GetAssetsByUserAsync(int userId);
    }

}
