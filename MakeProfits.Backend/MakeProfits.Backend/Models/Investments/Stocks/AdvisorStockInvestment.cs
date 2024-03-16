namespace MakeProfits.Backend.Models.Investments.Stocks
{
    public class AdvisorStockInvestment : StockInvestment
    {
        public Guid AdvisorID { get; set; }
        public decimal InitialFund { get; set; }
    }
}
