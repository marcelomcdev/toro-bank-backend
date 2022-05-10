using NUnit.Framework;
using System.Threading.Tasks;

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

            Assert.Fail();
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
