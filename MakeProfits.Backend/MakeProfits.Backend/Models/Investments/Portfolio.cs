using MakeProfits.Backend.Models.Investments.Bonds;
using MakeProfits.Backend.Models.Investments.MutualFunds;

namespace MakeProfits.Backend.Models.Investments.Stocks
{
    public class Portfolio
    {
        public List<StockInvestment> Stocks { get; set; }
        public decimal StocksInvestmentValue {  get; set; }
        public decimal StockInvestmentPercentage {  get; set; }
        public List<BondInvestment> Bonds { get; set; }
        public decimal BondInvestmentValue {  get; set; }
        public decimal BondInvestmentPercentage { get; set; }
        public List<MutualFundInvestment> MutualFunds { get; set; }
        public decimal MutualInvestmentValue { get; set; }
        public decimal MutualFundInvestmentPercentage { get; set; }
        public decimal TotalInvestmentValue { get; set; }


    }
}
