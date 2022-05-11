using FluentAssertions;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ToroBank.Application.Features.Transfer.Commands.ReceiveTransfer;
using ToroBank.Application.Features.Transfer.Commands.ReceiveTransfer.Objects;
using ToroBank.Application.Features.Users;
using ToroBank.Domain.Entities;
using ToroBank.Infrastructure.Persistence.Repositories;

namespace Application.UnitTests.UseCases
{
    public class TransferUseCaseTest
    {
        private Mock<IUserRepository> mockUserRepository = new Mock<IUserRepository>();
        private User mockUser;
        private ReceiveTransferCommand sut;
        private CancellationTokenSource _tokenSource;
        private CancellationToken _ct;

        [SetUp]
        public void Setup()
        {
            mockUserRepository = new Mock<IUserRepository>();
            mockUser = new User(300123, "Marcelo Martins de Castro", "45358996060", 350);
            sut = new ReceiveTransferCommand()
            {
                Event = "TRANSFER",
                Amount = 1000M,
                Origin = new OriginTransferObjectCommand() { Bank = "033", Branch = "03312", CPF = "45358996060" },
                Target = new TargetTransferObjectCommand() { Bank = "352", Branch = "0001", Account = "300123" }
            };

            mockUserRepository.Setup(repo => repo.UpdateAsync(It.IsAny<User>())).Returns(Task.FromResult(mockUser));
            mockUserRepository.Setup(repo => repo.GetByCPFAsync(It.IsAny<string>())).Returns(Task.FromResult(mockUser));

            _tokenSource = new CancellationTokenSource();
            _ct = _tokenSource.Token;

        }

        private void ExecuteHandler()
        {
            var handler = new ReceiveTransferCommandHandler(mockUserRepository.Object);
            var result = handler.Handle(sut, _ct).Result;
        }



        /// <summary>
        /// Eu, como investidor, gostaria de poder depositar um valor na minha conta Toro, através de PiX ou TED bancária, para que eu possa realizar investimentos.
        /// A Toro já participa do Sistema Brasileiro de Pagamentos (SPB) do Banco Central, e está integrado a ele. Isto significa que a Toro tem um número de banco (352), cada cliente tem um número único de conta na Toro, 
        /// e que toda transferência entre bancos passa pelo SBP do Banco Central, e quando a transferência é identificada como tendo o destino o banco Toro (352), uma requisição HTTP é enviada pelo Banco Central notificando tal evento. 
        /// O formato desta notificação segue o padrão REST + JSON a seguir (hipotético para efeito de simplificação do desafio):
        /// 
        /// Outra restrição é que a origem da transferência deve sempre ser do mesmo CPF do usuário na Toro.
        /// </summary>
        [Test]
        public void Should_pass_when_transfer_is_valid()
        {
            var handler = new ReceiveTransferCommandHandler(mockUserRepository.Object);
            var result = handler.Handle(sut, _ct).Result;

            sut.Should().NotBeNull();
            sut.Event.Should().NotBeNull();
            sut.Event.Should().NotBeEmpty();

            sut.Origin.CPF.Should().NotBeNull();
            sut.Origin.CPF.Should().Match(mockUser.CPF);

            sut.Amount.Should().NotBe(0);

            mockUser.Balance.Should().Be(1350);
        }

        [Test]
        public void Should_pass_if_balance_is_eq_1350()
        {
            var handler = new ReceiveTransferCommandHandler(mockUserRepository.Object);
            var result = handler.Handle(sut, _ct).Result;
            mockUser.Balance.Should().Be(1350);
        }

        


    }






}
