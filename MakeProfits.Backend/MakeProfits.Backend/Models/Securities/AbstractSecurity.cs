namespace MakeProfits.Backend.Models.Securities
{
    public class AbstractSecurity
    {
        public string TickerSymbol { get; set; }
        public string Cik { get; set; }
        public string OrganizationName { get; set; }
        public decimal Price { get; set; }
    }
}
