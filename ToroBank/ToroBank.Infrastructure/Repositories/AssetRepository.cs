using Microsoft.EntityFrameworkCore;
using ToroBank.Application.Common.Wrappers;
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

        public async Task<Asset?> GetAssetBySymbol(string symbol)
        {
            return await _assets.Where(f => f.Name == symbol).FirstOrDefaultAsync();
        }

        public async Task<PagedResponse<NegotiatedAsset>> GetMostNegotiatedAsset(int pageNumber, int pageSize)
        {
            var mockNegotiatedAssets = new List<NegotiatedAsset>();

            List<Asset> mockAssetBase = _assets.ToList();

            Random rndQuantity = new Random(30);
            Random rndUser = new Random(7);
            Random rndAsset = new Random(5);
            Random rndDate = new Random();

            DateTime start = DateTime.Now.AddDays(-7);
            int range = (DateTime.Today - start).Days;

            for (int i = 0; i < 100; i++)
            {
                int assetId = rndAsset.Next(1, 6);
                if (mockAssetBase.Any(f => f.Id == assetId))
                {
                    mockNegotiatedAssets.Add(new NegotiatedAsset { 
                        Id = Guid.NewGuid(), UserId = rndUser.Next(1, 7), 
                        Asset = mockAssetBase.FirstOrDefault(f => f.Id == assetId),
                        Quantity = rndQuantity.Next(1, 30), 
                        AcquiredAt = start.AddDays(rndDate.Next(range)) });
                }
            }

            for (int i = 1; i <= mockAssetBase.Count(); i++)
            {
                if (mockNegotiatedAssets.Count(f => f.Asset.Id == i) == 0)
                {
                    mockNegotiatedAssets.Add(new NegotiatedAsset { 
                        Id = Guid.NewGuid(), UserId = rndUser.Next(1, 7), 
                        Asset = mockAssetBase.FirstOrDefault(f => f.Id == i), 
                        Quantity = rndQuantity.Next(1, 30),
                        AcquiredAt = start.AddDays(rndDate.Next(range)) });
                }
            }

            var mostNegotiatedAssetsFroLastSevenDays = (from x in mockNegotiatedAssets
                                                            .Where(f => f.AcquiredAt >= DateTime.Now.AddDays(-7).Date && f.AcquiredAt <= DateTime.Now.Date)
                                                            .GroupBy(f => f.Asset)
                                                        select new NegotiatedAsset { Asset = x.First().Asset, Quantity = x.Sum(f => f.Quantity) })
                                                            .OrderByDescending(f => f.Quantity).ToList();

            int totalCount = mostNegotiatedAssetsFroLastSevenDays.Count;

            var data = mostNegotiatedAssetsFroLastSevenDays
                .OrderBy(o=> o.Quantity)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return new PagedResponse<NegotiatedAsset>(pageNumber, pageSize, totalCount, data);
        }
    }
}
