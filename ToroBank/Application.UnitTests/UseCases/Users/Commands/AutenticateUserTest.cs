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

namespace Application.UnitTests.UseCases.Users.Commands
{
    public class AutenticateUserTest
    {
        private Mock<IAuthRepository> _mockAuthRepository = new Mock<IAuthRepository>();
        private Mock<IUserRepository> _mockUserRepository = new Mock<IUserRepository>();
        private User mockUser;
        private AuthUserCommand _sut;
        private CancellationTokenSource _tokenSource;
        private CancellationToken _ct;

        [SetUp]
        public void Setup()
        {
            mockUser = new User(300123, "Marcelo", "45358996060", 350);
            
            _mockUserRepository = new Mock<IUserRepository>();
            _mockUserRepository.Setup(repo => repo.GetByEmailAsync(It.IsAny<string>())).Returns(Task.FromResult(mockUser));
        }

        [Test]
        public async Task Should_return_a_token_if_email_and_password_is_valid()
        {
            mockUser.Username = "marcelo.castro@gmail.com";
            mockUser.Password = "123456";
            var user = await _mockUserRepository.Object.GetByEmailAsync(mockUser.Username);

            Assert.IsNotNull(user);
            Assert.AreEqual(mockUser.Username, user.Username);
            Assert.AreEqual(mockUser.Password, user.Password);
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
