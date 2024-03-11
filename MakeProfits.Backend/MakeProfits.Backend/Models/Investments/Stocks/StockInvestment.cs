namespace MakeProfits.Backend.Models.Investments.Stocks
{
    public class StockInvestment
    {
        public int ClientID { get; set; }
        public int StratergyID {  get; set; }
        public int StockID { get; set; }
        public string StockName {  get; set; }
        public decimal StockPrice { get; set; }
        public int StockQuantity { get; set; }
        public decimal InvestmentValue { get; set; }
    }
}
