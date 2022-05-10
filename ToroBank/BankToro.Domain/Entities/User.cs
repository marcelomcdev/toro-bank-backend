using ToroBank.Domain.Common;

namespace ToroBank.Domain.Entities
{
    public class User : BaseEntity
    {
        protected User() { }
        public User(int accountNumber, string name, string cpf, decimal balance = 0M)
        {
            AccountNumber = accountNumber;
            Name = name;
            CPF = cpf;
            Balance = balance;
        }
        public int AccountNumber { get; set; }
        public string Name { get; set; }
        public string CPF { get; set; }
        public decimal Balance { get; set; }
    }
}
