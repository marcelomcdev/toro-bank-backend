using ToroBank.Domain.Common;

namespace ToroBank.Domain.Entities
{
    public class User : BaseAuditableEntity<int>
    {
        protected User() { }
        public User(int accountNumber, string name, string cpf, decimal balance = 0M)
        {
            AccountNumber = accountNumber;
            Name = name;
            CPF = cpf;
            Balance = balance;
        }
        public override int Id { get; set; }
        public int AccountNumber { get; set; }
        public string Name { get; set; }
        public string CPF { get; set; }
        public decimal Balance { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        
    }
}
