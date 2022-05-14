using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToroBank.Application.Common.DTOs.Assets;
using ToroBank.Application.Common.Interfaces.Repositories;
using ToroBank.Application.Common.Wrappers;
using ToroBank.Domain.Entities;

namespace ToroBank.Application.Features.Assets
{
    public interface IAssetRepository : IGenericRepository<Asset, int>
    {
        Task<PagedResponse<NegotiatedAsset>> GetMostNegotiatedAsset(int pageNumber, int pageSize);
    }
}
