namespace MakeProfits.Backend.Models.Securities
{
    public class SecurityIncomeStatement
    {
        public string TickerSymbol { get; set; }
        public int year { get; set; }
        public decimal NetIncome { get; set; }
        public decimal Revenue { get; set; }
    }
}
