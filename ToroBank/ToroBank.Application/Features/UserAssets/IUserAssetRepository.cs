using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToroBank.Application.Common.Interfaces.Repositories;
using ToroBank.Application.Common.Wrappers;
using ToroBank.Domain.Entities;

namespace ToroBank.Application.Features.UserAssets
{
    
    public interface IUserAssetRepository : IGenericRepository<UserAsset, int>
    {
        //Task<PagedResponse<UserAsset>> GetAssets(int pageNumber, int pageSize);
        Task<List<UserAsset>> GetAssetsByUserAsync(int userId);
    }

}
