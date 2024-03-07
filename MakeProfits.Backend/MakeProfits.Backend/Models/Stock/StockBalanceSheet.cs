namespace MakeProfits.Backend.Models.Stock
{
    public class StockBalanceSheet
    {
        public string TickerSymbol { get; set; }
        public int year {  get; set; }
        public decimal TotalAssets { get; set; }
        public decimal TotalStockholdersEquity { get; set; }
    }
}
