namespace MakeProfits.Backend.Models.Securities
{
    public class SecurityBalanceSheet
    {
        public string TickerSymbol { get; set; }
        public int year {  get; set; }
        public decimal TotalAssets { get; set; }
        public decimal TotalStockholdersEquity { get; set; }
    }
}
