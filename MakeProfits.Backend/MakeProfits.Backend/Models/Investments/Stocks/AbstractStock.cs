namespace MakeProfits.Backend.Models.Investments.Stocks
{
    public class AbstractStock
    {
        public string TickerSymbol { get; set; }
        public string Cik { get; set; }
        public string OrganizationName { get; set; }
        public decimal Price { get; set; }
    }
}
