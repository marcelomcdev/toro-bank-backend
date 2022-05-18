namespace ToroBank.Domain.Entities
{
    public class Position
    {
        public string Symbol { get; set; }
        public int Amount { get; set; }
        public decimal CurrentPrice { get; set; }
    }
}
