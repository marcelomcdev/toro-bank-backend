using MediatR;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ToroBank.Application.Common.Exceptions;
using ToroBank.Application.Common.Wrappers;
using ToroBank.Application.Features.Assets;
using ToroBank.Application.Features.UserAssets;
using ToroBank.Application.Features.Users;
using ToroBank.Domain.Entities;
using static Application.UnitTests.UseCases.Positions.Queries.Test.GetUserPositionDTO;

namespace Application.UnitTests.UseCases.Positions.Queries
{

    public class GetUserPositions
    {
        private Mock<IUserRepository> mockUserRepository = new Mock<IUserRepository>();
        private Mock<IAssetRepository> mockAssetRepository = new Mock<IAssetRepository>();
        private Mock<IUserAssetRepository> mockUserAssetRepository = new Mock<IUserAssetRepository>();
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
        private GetUserPositionQuery sut;
        private CancellationTokenSource _tokenSource;
        private CancellationToken _ct;
        

        [SetUp]
        public void Setup()
        {
            mockUserRepository = new Mock<IUserRepository>();
            mockAssetRepository = new Mock<IAssetRepository>();
            mockUserAssetRepository = new Mock<IUserAssetRepository>();

            mockUserPosition = new UserPosition();
            mockUser = new User(300123, "Marcelo", "45358996060", 234);
            mockAssetsOfUser = new List<UserAsset>() {
                new UserAsset(1,1,2),
                new UserAsset(4,1,3)
            };

            mockAssetRepository.Setup(repo => repo.GetAllAsync().Result).Returns(mockAssets);

            mockAssetsOfUser.ForEach(f => mockUserPosition.Positions.Add(
                new Position()
                {
                    Amount = f.Quantity,
                    Symbol = mockAssets.FirstOrDefault(e => e.Id == f.Id)?.Name,
                    CurrentPrice = mockAssets.FirstOrDefault(e => e.Id == f.Id)?.Value ?? 0
                }));

            sut = new GetUserPositionQuery();
            sut.Id = 1;

        }


        [Test]
        public void Should_pass_if_have_two_positions()
        {
            mockUserRepository.Setup(repo => repo.GetByIdAsync(It.IsNotIn(new int[] { 0 }))).Returns(Task.FromResult(mockUser));
            mockUserAssetRepository.Setup(repo => repo.GetAllAsync().Result).Returns(mockAssetsOfUser);
            mockAssetRepository.Setup(repo => repo.GetAllAsync().Result).Returns(mockAssets);

            var handler = new GetUserPositionQueryHandler(mockUserRepository.Object, mockAssetRepository.Object, mockUserAssetRepository.Object);
            var result = handler.Handle(sut, _ct).Result;

            Assert.AreEqual(2, result.Data.Positions.Count);
        }

        [Test]
        public async Task Should_pass_if_consolidated_value_is_eq_413_19()
        {
            mockUserRepository.Setup(repo => repo.GetByIdAsync(It.IsNotIn(new int[] { 0 }))).Returns(Task.FromResult(mockUser));
            mockUserAssetRepository.Setup(repo => repo.GetAllAsync().Result).Returns(mockAssetsOfUser);
            mockAssetRepository.Setup(repo => repo.GetAllAsync().Result).Returns(mockAssets);

            var handler = new GetUserPositionQueryHandler(mockUserRepository.Object, mockAssetRepository.Object, mockUserAssetRepository.Object);
            var result = await handler.Handle(sut, _ct);
            
            Assert.AreEqual(413.19, result.Data.Consolidated);
        }

        [Test]
        public void Should_throw_an_error_if_id_is_zero()
        {
            sut.Id = 0;
            mockUserRepository.Setup(repo => repo.GetByIdAsync(It.IsNotIn(new int[] { 0 }))).Returns(Task.FromResult(mockUser));
            mockUserAssetRepository.Setup(repo => repo.GetAllAsync().Result).Returns(mockAssetsOfUser);
            mockAssetRepository.Setup(repo => repo.GetAllAsync().Result).Returns(mockAssets);

            var handler = new GetUserPositionQueryHandler(mockUserRepository.Object, mockAssetRepository.Object, mockUserAssetRepository.Object);
            Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(sut, _ct));
        }


        [Test]
        public async Task Should_have_throw_error_if_user_not_found()
        {
            sut.Id = 54;
            mockUserRepository.Setup(repo => repo.GetByIdAsync(It.IsNotIn(new int[] { 0, 54 }))).Returns(Task.FromResult(mockUser));
            mockUserAssetRepository.Setup(repo => repo.GetAllAsync().Result).Returns(mockAssetsOfUser);
            mockAssetRepository.Setup(repo => repo.GetAllAsync().Result).Returns(mockAssets);

            var handler = new GetUserPositionQueryHandler(mockUserRepository.Object, mockAssetRepository.Object, mockUserAssetRepository.Object);
            Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(sut, _ct));
        }


    }


    
    
}





namespace Application.UnitTests.UseCases.Positions.Queries.Test
{
    public record GetUserPositionDTO(
    List<Position> Positions, decimal CheckingAccountAmount, decimal Consolidated)
    {
        public static GetUserPositionDTO ToDto(UserPosition userPosition)
        {
            return new GetUserPositionDTO(
                userPosition.Positions,
                userPosition.CheckingAccountAmount,
                userPosition.Consolidated
                );
        }

        public class GetUserPositionQuery : IRequest<Result<GetUserPositionDTO>>
        {
            public int Id { get; set; }
        }


        public class GetUserPositionQueryHandler : IRequestHandler<GetUserPositionQuery, Result<GetUserPositionDTO>>
        {
            private readonly IUserRepository _userRepository;
            private readonly IAssetRepository _assetRepository;
            private readonly IUserAssetRepository _userAssetRepository;

            public GetUserPositionQueryHandler(IUserRepository userRepository, IAssetRepository assetRepository, IUserAssetRepository userAssetRepository)
            {
                _userRepository = userRepository;
                _assetRepository = assetRepository;
                _userAssetRepository = userAssetRepository;
            }

            public async Task<Result<GetUserPositionDTO>> Handle(GetUserPositionQuery query, CancellationToken cancellationToken)
            {
                var user = await _userRepository.GetByIdAsync(query.Id);
                if (user == null)
                {
                    throw new NotFoundException(nameof(User), query.Id);
                }

                var assets = await _assetRepository.GetAllAsync();
                var assetsOfUser = _userAssetRepository.GetAllAsync().Result.Where(f=> f.Id == user.Id).ToList();

                UserPosition up = new UserPosition();
                assetsOfUser.ForEach(f => up.Positions.Add(new Position() {
                    Amount = f.Quantity,
                    Symbol = assets.FirstOrDefault(g=> g.Id == f.AssetId)?.Name ?? "",
                    CurrentPrice = assets.FirstOrDefault(g => g.Id == f.AssetId)?.Value ?? 0
                }));

                up.CheckingAccountAmount = user.Balance;
                assetsOfUser.ForEach(f => up.Consolidated += ((f.Quantity * assets.FirstOrDefault(e => e.Id == f.AssetId)?.Value ?? 0)));
                up.Consolidated += up.CheckingAccountAmount;

                var dto = GetUserPositionDTO.ToDto(up);

                return Result.Ok(dto);
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
}