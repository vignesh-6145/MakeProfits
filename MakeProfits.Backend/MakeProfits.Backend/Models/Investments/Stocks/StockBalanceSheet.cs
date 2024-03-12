namespace MakeProfits.Backend.Models.Investments.Stocks
{
    public class StockBalanceSheet
    {
        public string TickerSymbol { get; set; }
        public int year { get; set; }
        public decimal TotalAssets { get; set; }
        public decimal TotalStockholdersEquity { get; set; }
    }
}
