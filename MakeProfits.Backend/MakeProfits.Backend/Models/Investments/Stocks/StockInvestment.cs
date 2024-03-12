namespace MakeProfits.Backend.Models.Investments.Stocks
{
    public class StockInvestment
    {
        public Guid ClientID { get; set; }
        public Guid StratergyID {  get; set; }
        public Guid StockID { get; set; }
        public string StockName {  get; set; }
        public decimal StockPrice { get; set; }
        public int StockQuantity { get; set; }
        public decimal InvestmentValue { get; set; }
    }
}
