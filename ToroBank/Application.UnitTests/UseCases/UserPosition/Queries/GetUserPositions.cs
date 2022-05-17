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
        private List<Asset> mockAssets;
        private UserPosition mockUserPosition;

        [SetUp]
        public void Setup()
        {            
            mockUserRepository = new Mock<IUserRepository>();
            mockAssetRepository = new Mock<IAssetRepository>();

            mockUser = new User(300123, "Marcelo", "45358996060", 234);
            mockAssetsOfUser = new List<UserAsset>() {
                new UserAsset(1,1,2),
                new UserAsset(4,1,3)
            };

            mockAssets = mockAssetRepository.Object.GetAllAsync().Result.ToList();

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
            Assert.Fail();
        }

        [Test]
        public void Should_pass_if_consolidated_value_is_eq_413_19()
        {
            Assert.Fail();
        }

        [Test]
        public void Should_pass_if_positions_have_total_value_is_eq_179_19()
        {
            Assert.Fail();
        }

    }

    public class UserPosition
    {
        public double CheckingAccountAmount { get; set; }
        public double Consolidated { get; set; }
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
