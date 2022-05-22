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
        const string fakeToken = "eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjEwMTEiLCJqdGkiOiJkNTM4Yzg2N2JlZTc0YTIyYmFjN2VhNzZjZTQxMzNiYSIsInJvbGUiOiIyMDAwIiwibmJmIjoxNjUzMTgwMzY1LCJleHAiOjE2NTMxODM5NjUsImlhdCI6MTY1MzE4MDM2NSwiaXNzIjoiQVBJIiwiYXVkIjoiQUdlbnRlRW1DYXNhIn0.aKnlJtdJJD7XqjlYkuhc3-viq_GdJOQTfg88lK7L7x-UKSkhJAxbVxGLaUP3zVVjU1Q3nShmPVmxlo60nO-WuEqwhqw_McT-B-XZ3tvFWGDLD1EjFC9VCenR5ygDIDniEHBi2QKhkPwhEDco1rg4IaFpN4caIk0b2Ik-_AiQKYJF91ac_vvRbcN9CxFCDRN-GE_-k4hfPbPbBGmapC9qTdHLA3JZKfI_RUDAy88ILcntTdsVH9rDCjNUaeaenGeZv__4OhMPyn0FqNMoPH59Et0rx3_gDpi8Fk67HDh8Ib1TJuXGVoF3rVfv-BqVoFsrNotTv_-5bYFAAvuwwJeirQ";
        
        [SetUp]
        public void Setup()
        {
            mockUser = new User(300123, "Marcelo", "45358996060", 350);
            
            mockToken = new Token
            {
                AccessToken = fakeToken,
                Client = "Marcelo Martins de Castro",
                UID = "1"
            };

            _mockUserRepository = new Mock<IUserRepository>();
            _mockTokenService = new Mock<ITokenService>();
            _mockUserRepository.Setup(repo => repo.GetByEmailAsync(It.IsAny<string>())).Returns(Task.FromResult(mockUser));
            _mockTokenService.Setup(repo => repo.IsValid(It.IsAny<TokenRequest>())).Returns(Task.FromResult(true));
            _mockTokenService.Setup(repo => repo.GenerateJwtToken(It.IsAny<string>())).Returns(Task.FromResult(mockToken));
            _mockTokenService.Setup(repo => repo.Authenticate(It.IsAny<TokenRequest>())).Returns(Task.FromResult(new TokenResponse() { token = mockToken }));
        }

        [Test]
        public async Task Should_return_a_token_if_email_and_password_is_valid()
        {
            mockUser.Username = "marcelo.castro@gmail.com";
            mockUser.Password = "123456";

            var handler = new AuthUserCommandHandler(_mockAuthRepository.Object, _mockTokenService.Object);
            var result = handler.Handle(_sut, _ct).Result;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Data);
            Assert.AreEqual(fakeToken, result.Data.AccessToken);
            Assert.AreEqual("Marcelo Martins de Castro", result.Data.Client);
            Assert.AreEqual("1", result.Data.UID);
        }

        [Test]
        public void Should_return_an_error_if_email_is_null()
        {

            Assert.Fail();
        }


        [Test]
        public void Should_return_an_error_if_password_is_is_null()
        {
            Assert.Fail();
        }

        [Test]
        public void Should_return_an_error_if_email_is_empty()
        {
            Assert.Fail();
        }


        [Test]
        public void Should_return_an_error_if_password_is_is_empty()
        {
            Assert.Fail();
        }

        [Test]
        public void Should_return_an_error_if_email_is_invalid()
        {
            Assert.Fail();
        }


        [Test]
        public void Should_return_an_error_if_password_is_invalid()
        {
            Assert.Fail();
        }
    }

}
