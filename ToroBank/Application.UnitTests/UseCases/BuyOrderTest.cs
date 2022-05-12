using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToroBank.Domain.Entities;

namespace Application.UnitTests.UseCases
{
    public class BuyOrderTest
    {
        private List<Asset> assets;
        //private List<NegotiatedAsset> mostNegotiatedAssets;

        private List<Asset> mockAssetBase;
        private List<NegotiatedAssetItem> mockNegotiatedAssets;
        private List<MostNegotiatedAssetItem> mockMostNegotiatedAssetsFroLastSevenDays;

        public class NegotiatedAssetItem
        {
            public Guid Id { get; set; }
            public int UserId { get; set; }
            public Asset Asset { get; set; }
            public int Quantity { get; set; }
            public DateTime AcquiredAt { get; set; }
        }

        public class Asset
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public decimal Value { get; set; }
        }

        public class MostNegotiatedAssetItem
        {
            public Asset Asset { get; set; }
            public int Quantity { get; set; }
        }


        [SetUp]
        public void Setup()
        {
            mockNegotiatedAssets = new List<NegotiatedAssetItem>();

            var mockAssetBase = new List<Asset>() {
                new Asset{ Id =1, Name = "PETR4", Value = 28.44M },
                new Asset{ Id =2, Name = "MGLU3", Value = 28.44M },
                new Asset{ Id =3, Name = "VVAR3", Value = 25.91M },
                new Asset{ Id =4, Name = "SANB11", Value = 25.91M },
                new Asset{ Id =5, Name = "TORO4", Value = 115.98M },
            };

            //Random values
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
                    mockNegotiatedAssets.Add(new NegotiatedAssetItem { Id = Guid.NewGuid(), UserId = rndUser.Next(1, 7), Asset = mockAssetBase.FirstOrDefault(f => f.Id == assetId), Quantity = rndQuantity.Next(1, 30), AcquiredAt = start.AddDays(rndDate.Next(range)) });
                }
            }

            //validation: if one or more assets was not added in list, add a registry
            for (int i = 1; i <= 5; i++)
            {
                if (mockNegotiatedAssets.Count(f => f.Asset.Id == i) == 0)
                {
                    mockNegotiatedAssets.Add(new NegotiatedAssetItem { Id = Guid.NewGuid(), UserId = rndUser.Next(1, 7), Asset = mockAssetBase.FirstOrDefault(f => f.Id == i), Quantity = rndQuantity.Next(1, 30), AcquiredAt = start.AddDays(rndDate.Next(range)) });
                }
            }

            mockMostNegotiatedAssetsFroLastSevenDays = (from x in mockNegotiatedAssets
                                                            .Where(f => f.AcquiredAt >= DateTime.Now.AddDays(-7).Date && f.AcquiredAt <= DateTime.Now.Date)
                                                            .GroupBy(f => f.Asset)
                                         select new MostNegotiatedAssetItem { Asset = x.First().Asset, Quantity = x.Sum(f => f.Quantity) })
                                                            .OrderByDescending(f => f.Quantity).ToList();
            
        }
       

        [Test]
        public void Should_have_only_5_most_negotiated_assets_from_last_7_days()
        {
            Assert.IsNotNull(mockMostNegotiatedAssetsFroLastSevenDays);
            Assert.IsTrue(mockMostNegotiatedAssetsFroLastSevenDays.Count() == 5);
        }

        private List<MostNegotiatedAssetItem> ReturnMostNegotiatedAssetsFromLastDays(int days)
        {
            var mostNegotiatedOrdered = (from x in mockNegotiatedAssets
                                                            .Where(f => f.AcquiredAt >= DateTime.Now.AddDays(-days).Date && f.AcquiredAt <= DateTime.Now.Date)
                                                            .GroupBy(f => f.Asset)
                                         select new MostNegotiatedAssetItem { Asset = x.First().Asset, Quantity = x.Sum(f => f.Quantity) })
                                                            .OrderByDescending(f => f.Quantity).ToList();
            return mostNegotiatedOrdered;
        }

        [Test]
        public void Should_have_pass_if_conditions_are_valid()
        {
            Assert.Fail();
        }
    }
}
