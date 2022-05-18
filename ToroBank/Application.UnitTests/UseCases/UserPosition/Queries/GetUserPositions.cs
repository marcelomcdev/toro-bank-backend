using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using ToroBank.Application.Features.Assets;
using ToroBank.Application.Features.Users;
using ToroBank.Domain.Entities;

namespace Application.UnitTests.UseCases.UserPosition.Queries
{
    
    public class GetUserPositions
    {
        private Mock<IUserRepository> mockUserRepository = new Mock<IUserRepository>();
        private Mock<IAssetRepository> mockAssetRepository = new Mock<IAssetRepository>();
        private User mockUser;
        private List<UserAsset> mockAssetsOfUser;
        private IReadOnlyList<Asset> mockAssets = new List<Asset>() {
                new Asset(1, "PETR4", 28.44M ),
                new Asset(2, "MGLU3", 25.91M ),
                new Asset(3, "VVAR3", 25.91M ),
                new Asset(4, "SANB11", 40.77M ),
                new Asset(5, "TORO4", 115.98M ),
            };
        private UserPosition mockUserPosition;

        [SetUp]
        public void Setup()
        {            
            mockUserRepository = new Mock<IUserRepository>();
            mockAssetRepository = new Mock<IAssetRepository>();
            mockUserPosition = new UserPosition();
            mockUser = new User(300123, "Marcelo", "45358996060", 234);
            mockAssetsOfUser = new List<UserAsset>() {
                new UserAsset(1,1,2),
                new UserAsset(4,1,3)
            };

            mockAssetRepository.Setup(repo => repo.GetAllAsync().Result).Returns(mockAssets);

            mockAssetsOfUser.ForEach(f => mockUserPosition.Positions.Add(
                new Position() {
                    Amount = f.Quantity,
                    Symbol = mockAssets.FirstOrDefault(e => e.Id == f.Id)?.Name,
                    CurrentPrice = mockAssets.FirstOrDefault(e => e.Id == f.Id)?.Value ?? 0
                }));
       }


        [Test]
        public void Should_pass_if_have_two_positions()
        {
            Assert.AreEqual(2, mockUserPosition.Positions.Count);
        }

        [Test]
        public void Should_pass_if_consolidated_value_is_eq_413_19()
        {
            mockUserPosition.CheckingAccountAmount = 234.00M;
            mockAssetsOfUser.ForEach(f => mockUserPosition.Consolidated += ((f.Quantity * mockAssets.FirstOrDefault(e => e.Id == f.AssetId)?.Value ?? 0) ));
            mockUserPosition.Consolidated += mockUserPosition.CheckingAccountAmount;
            Assert.AreEqual(413.19, mockUserPosition.Consolidated);
        }

    }

    public class UserPosition
    {
        public decimal CheckingAccountAmount { get; set; }
        public decimal Consolidated { get; set; }
        public List<Position> Positions { get; set; }
        public UserPosition()
        {
            Positions = new List<Position>();
        }
    }

    public class Position
    {
        public string Symbol { get; set; }
        public int Amount { get; set; }
        public decimal CurrentPrice { get; set; }
    }


}
