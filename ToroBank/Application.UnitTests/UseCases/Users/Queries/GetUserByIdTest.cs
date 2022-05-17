using FluentAssertions;
using NUnit.Framework;
using ToroBank.Application.Common.Exceptions;
using ToroBank.Application.Features.Users.Queries.GetUserById;
using NUnit.Framework;
using System.Threading.Tasks;
using ToroBank.Domain.Entities;
using Moq;
using ToroBank.Application.Features.Users;
using System.Threading;

namespace Application.UnitTests.UseCases.Queries
{
    public class GetUserByIdTest
    {
        private Mock<IUserRepository> mockUserRepository = new Mock<IUserRepository>();
        private User mockUser;
        private GetUserByIdQuery sut;
        private CancellationTokenSource _tokenSource;
        private CancellationToken _ct;
        private GetUserByIdDTO _mockUserDto;

        [SetUp]
        public void Setup()
        {
            mockUser = new User(300123, "Marcelo", "45358996060", 350);
            mockUserRepository = new Mock<IUserRepository>();

            _mockUserDto = new GetUserByIdDTO(0, 300123, "Marcelo", "45358996060", 350);
            mockUserRepository.Setup(repo => repo.GetByIdAsync(It.IsNotIn(new int[] { 0 }))).Returns(Task.FromResult(mockUser));

            
            sut = new GetUserByIdQuery();
            sut.Id = 1;
        }

        [Test]
        public void Should_return_an_valid_user()
        {
            var handler = new GetUserByIdQueryHandler(mockUserRepository.Object);
            var result = handler.Handle(sut, _ct).Result;
           
            Assert.IsNotNull(result);
            Assert.AreEqual(_mockUserDto, result.Data);
        }

        [Test]
        
        public void Should_not_return_an_user_if_id_is_zero()
        {
            sut.Id = 0;
            var handler = new GetUserByIdQueryHandler(mockUserRepository.Object);
            Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(sut, _ct));
        }
    }
}
