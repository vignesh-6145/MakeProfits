namespace MakeProfits.Backend.Models.Investments.Stocks
{
    public class StockIncomeStatement
    {
        public string TickerSymbol { get; set; }
        public int year { get; set; }
        public decimal NetIncome { get; set; }
        public decimal Revenue { get; set; }
    }
}
