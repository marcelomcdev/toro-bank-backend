using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToroBank.Application.Features.Assets;
using ToroBank.Domain.Entities;
using ToroBank.Infrastructure.Persistence.Context;

namespace ToroBank.Infrastructure.Persistence.Repositories
{
    public class AssetRepository : GenericRepository<Asset, int>, IAssetRepository
    {
        private readonly DbSet<Asset> _assets;
        public AssetRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _assets = dbContext.Set<Asset>();
        }

    }
}
