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
using MediatR;
using ToroBank.Application.Common.Wrappers;
using ToroBank.Application.Common.Interfaces.Repositories;
using ToroBank.Domain.Common.Configuration;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Linq;
using ToroBank.Application.Features.Authentication;
using ToroBank.Application.Features.Authentication.Commands;
using ToroBank.Application.Common.Identity.Services;
using ToroBank.Application.Common.Identity.Models;
using System.IO;

namespace Application.UnitTests.UseCases.Users.Commands
{
    public class AutenticateUserTest
    {
        private Mock<IAuthRepository> _mockAuthRepository = new Mock<IAuthRepository>();
        private Mock<IUserRepository> _mockUserRepository = new Mock<IUserRepository>();
        private Mock<ITokenService> _mockTokenService = new Mock<ITokenService>();

        private Token mockToken;
        private User mockUser;
        private AuthUserCommand _sut;
        private CancellationTokenSource _tokenSource;
        private CancellationToken _ct;
        const string fakeToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCIsImN0eSI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjEiLCJqdGkiOiIyNmE0N2ZmN2QwNWY0ODM3OWE0NWM0ZjE0OTVkY2I1MiIsInJvbGUiOiJVc2VyIiwibmJmIjoxNjUzMTg1MzU5LCJleHAiOjE2NTMxODg5NTksImlhdCI6MTY1MzE4NTM1OSwiaXNzIjoidG9yby1iYW5rIiwiYXVkIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NzAyNSJ9.G2P2kQIQ1H1m8A5h1VsolO64pkV5ZqRpck_S5Ge1IKg";
        
        [SetUp]
        public void Setup()
        {
            mockUser = new User(300123, "Marcelo", "45358996060", 350);
            mockUser.Id = 1;
            mockToken = new Token
            {
                AccessToken = fakeToken,
                Expires = DateTime.Parse("2022-05-22T03:09:19.2797325Z"),
                //UID = "1"
            };

            _mockUserRepository = new Mock<IUserRepository>();
            _mockTokenService = new Mock<ITokenService>();
            _mockUserRepository.Setup(repo => repo.GetByEmailAsync(It.IsAny<string>())).Returns(Task.FromResult(mockUser));
            _mockTokenService.Setup(repo => repo.IsValid(It.IsAny<TokenRequest>())).Returns(Task.FromResult(true));
            _mockTokenService.Setup(repo => repo.GenerateJwtToken(It.IsAny<string>())).Returns(Task.FromResult(mockToken));
            _mockTokenService.Setup(repo => repo.Authenticate(It.IsAny<TokenRequest>())).Returns(Task.FromResult(new TokenResponse() { token = mockToken }));
            _mockTokenService.Setup(repo => repo.GetIdByToken(It.IsAny<string>())).Returns(Task.FromResult(1));
        }

        [Test]
        public async Task Should_return_a_token_if_email_and_password_is_valid()
        {
            mockUser.Username = "marcelo.castro@gmail.com";
            mockUser.Password = "123456";
            _sut = new AuthUserCommand() { Email = mockUser.Username, Password = mockUser.Password };

            var handler = new AuthUserCommandHandler(_mockAuthRepository.Object, _mockTokenService.Object);
            var result = handler.Handle(_sut, _ct).Result;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Data);
            Assert.AreEqual(fakeToken, result.Data.AccessToken);
            //Assert.AreEqual("1", result.Data.UID);
            Assert.AreEqual(mockToken, _mockTokenService.Object.GenerateJwtToken(mockUser.Username).Result);
            Assert.AreEqual(1, _mockTokenService.Object.GetIdByToken(result.Data.AccessToken).Result);
        }

        [Test]
        public void Should_return_an_error_if_email_is_null()
        {
            mockUser.Username = "";
            mockUser.Password = "123456";

            _mockTokenService.Setup(repo => repo.IsValid(It.IsAny<TokenRequest>())).Returns(Task.FromResult(false));
            _mockTokenService.Setup(repo => repo.Authenticate(It.IsAny<TokenRequest>())).Throws<NotFoundException>();
            var handler = new AuthUserCommandHandler(_mockAuthRepository.Object, _mockTokenService.Object);
            Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(_sut, _ct));
        }


        [Test]
        public void Should_return_an_error_if_password_is_is_null()
        {
            mockUser.Username = "marcelo.castro@gmail.com";
            mockUser.Password = null;

            _mockTokenService.Setup(repo => repo.IsValid(It.IsAny<TokenRequest>())).Returns(Task.FromResult(false));
            _mockTokenService.Setup(repo => repo.Authenticate(It.IsAny<TokenRequest>())).Throws<NotFoundException>();
            var handler = new AuthUserCommandHandler(_mockAuthRepository.Object, _mockTokenService.Object);
            Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(_sut, _ct));
        }

        [Test]
        public void Should_return_an_error_if_email_is_empty()
        {
            mockUser.Username = "";
            mockUser.Password = "123456";

            _mockTokenService.Setup(repo => repo.IsValid(It.IsAny<TokenRequest>())).Returns(Task.FromResult(false));
            _mockTokenService.Setup(repo => repo.Authenticate(It.IsAny<TokenRequest>())).Throws<NotFoundException>();
            var handler = new AuthUserCommandHandler(_mockAuthRepository.Object, _mockTokenService.Object);
            Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(_sut, _ct));
        }


        [Test]
        public void Should_return_an_error_if_password_is_is_empty()
        {
            mockUser.Username = "marcelo.castro@gmail.com";
            mockUser.Password = "";

            _mockTokenService.Setup(repo => repo.IsValid(It.IsAny<TokenRequest>())).Returns(Task.FromResult(false));
            _mockTokenService.Setup(repo => repo.Authenticate(It.IsAny<TokenRequest>())).Throws<NotFoundException>();
            var handler = new AuthUserCommandHandler(_mockAuthRepository.Object, _mockTokenService.Object);
            Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(_sut, _ct));
        }

        [Test]
        public void Should_return_an_error_if_email_is_invalid()
        {
            mockUser.Username = "marcelo.castro@xxx.com";
            mockUser.Password = "123456";

            _mockTokenService.Setup(repo => repo.IsValid(It.IsAny<TokenRequest>())).Returns(Task.FromResult(false));
            _mockTokenService.Setup(repo => repo.Authenticate(It.IsAny<TokenRequest>())).Throws<NotFoundException>();
            var handler = new AuthUserCommandHandler(_mockAuthRepository.Object, _mockTokenService.Object);
            Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(_sut, _ct));
        }


        [Test]
        public void Should_return_an_error_if_password_is_invalid()
        {
            mockUser.Username = "marcelo.castro@gmail.com";
            mockUser.Password = "123456789";

            _mockTokenService.Setup(repo => repo.IsValid(It.IsAny<TokenRequest>())).Returns(Task.FromResult(false));
            _mockTokenService.Setup(repo => repo.Authenticate(It.IsAny<TokenRequest>())).Throws<NotFoundException>();
            var handler = new AuthUserCommandHandler(_mockAuthRepository.Object, _mockTokenService.Object);
            Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(_sut, _ct));
        }
    }

}
