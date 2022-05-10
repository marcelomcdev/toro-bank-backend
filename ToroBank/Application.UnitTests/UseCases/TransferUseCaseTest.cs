using FluentAssertions;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToroBank.Domain.Entities;

namespace Application.UnitTests.UseCases
{
    public class TransferUseCaseTest
    {
        [SetUp]
        public void Setup()
        {
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
            //o modelo de transferencia deve ser válido
            var mock = new TransferRequest()
            {
                Event = "TRANSFER",
                Amount = 1000M,
                Origin = new OriginTransferObject() { Bank = "033", Branch = "03312", CPF = "45358996060" },
                Target = new TargetTransferObject() { Bank = "352", Branch = "0001", Account = "300123" }
            };

            //deve ser o mesmo cpf do usuario
            var repository = new UserRepository();
            var user = repository.GetByCPFAsync(mock.Origin.CPF);

            //deve gravar o valor do saldo
            if(user != null)
            {
                user.Balance += mock.Amount;
                repository.UpdateAsync(user);
            }

            user.Should().NotBeNull();
            user?.Balance.Should().Be(1000);

            mock.Should().NotBeNull();
            mock.Amount.Should().NotBe(0);
            mock.Origin.CPF.Should().NotBeNull();
            mock.Origin.CPF.Should().Match(mock.Origin.CPF);
        }
    }

    public interface IUserRepository
    {
        User GetByCPFAsync(string cpf);
        Task<User> UpdateAsync(User user);
    }

    public class UserRepository : IUserRepository
    {
        public User? GetByCPFAsync(string cpf)
        {
            var list = new List<User>() { new User(300123,"Marcelo", "12345678999",0), new User(300124, "João", "45358996060") };
            return list.FirstOrDefault(f => f.CPF.Equals(cpf, System.StringComparison.Ordinal));
        }

        public Task<User> UpdateAsync(User user)
        {
            user.Id += 1;
            return Task.FromResult(user);
        }
    }

    public class TransferRequest
    {
        public string? Event { get; set; }
        public TargetTransferObject? Target { get; set; }
        public OriginTransferObject? Origin { get; set; }
        public decimal Amount { get; set; }
    }

    public class TargetTransferObject : BaseTransferObject
    {
        public string? Account { get; set; }
    }

    public class OriginTransferObject : BaseTransferObject
    {
        public string? CPF { get; set; }
    }

    public abstract class BaseTransferObject
    {
        public string? Bank { get; set; }
        public string? Branch { get; set; }
    }





}
