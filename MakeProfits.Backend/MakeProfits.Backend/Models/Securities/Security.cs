namespace MakeProfits.Backend.Models.Securities
{
    public class Security
    {
        public int SecurityID { get; set; }
        public string TickerSymbol { get; set; }
        public string Cik { get; set; }
        public string OrganizationName { get; set; }
        public decimal Price { get; set; }
        public DateTime UpdatedOn { get; set; } = DateTime.Now;
    }
}
