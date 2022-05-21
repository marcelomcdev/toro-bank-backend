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

namespace Application.UnitTests.UseCases.Users.Commands
{
    public class AutenticateUserTest
    {

        [SetUp]
        public void Setup()
        {
            
        }

        [Test]
        public void Should_return_a_token_if_email_and_password_is_valid()
        {
            Assert.Fail();
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
