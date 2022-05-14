using Moq;
using NUnit.Framework;
using System.Threading.Tasks;
using ToroBank.Application.Features.Users;
using ToroBank.Domain.Entities;

namespace Application.UnitTests.UseCases.Queries
{
    public class GetUserByIdUseCaseTest
    {
        private Mock<IUserRepository> mockUserRepository = new Mock<IUserRepository>();
        private User mockUser;
        [SetUp]
        public void Setup()
        {
            mockUser = new User(300123, "Marcelo Martins de Castro", "45358996060", 350);
            mockUserRepository = new Mock<IUserRepository>();
            mockUserRepository.Setup(repo => repo.GetByIdAsync(It.IsNotIn(new int[] { 0 }))).Returns(Task.FromResult(mockUser));
        }

        [Test]
        public void Should_return_an_user()
        {
            var user = mockUserRepository.Object.GetByIdAsync(1);
            Assert.IsNotNull(user);
            Assert.AreEqual(mockUser, user.Result);
        }

        [Test]
        public void Should_not_return_an_user_if_id_is_zero()
        {
            var user = mockUserRepository.Object.GetByIdAsync(0);
            Assert.IsNull(user.Result);
        }
    }
}
