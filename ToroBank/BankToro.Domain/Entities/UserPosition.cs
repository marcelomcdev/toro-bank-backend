namespace ToroBank.Domain.Entities
{
    public class UserPosition
    {
        public decimal CheckingAccountAmount { get; set; }
        public decimal Consolidated { get; set; }
        public List<Position> Positions { get; set; }
        public decimal Investments { get; set; }
        public UserPosition()
        {
            Positions = new List<Position>();
        }
    }
}
