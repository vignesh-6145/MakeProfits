using MakeProfits.Backend.Models.Investments.Bonds;
using MakeProfits.Backend.Models.Investments.MutualFunds;
using MakeProfits.Backend.Models.Investments.Stocks;

namespace MakeProfits.Backend.Models.Investments
{
    public class AdvisorPortfolio
    {
        public List<AdvisorStockInvestment> StockInvestments { get; set; }
        public decimal StockInvestmentsValue {  get; set; }
        public decimal StockInvestmentsPercentage {  get; set; }

        public List<AvisorBondInvestment> BondInvestments { get; set; }
        public decimal BondInvestmentsValue { get;set; }
        public decimal BondInvestmentsPercentage { get; set; }

        public List<AdvisorMutualFundInvestment> MutualFundInvestments { get; set; }
        public decimal MutualFundInvestmentsValue { get; set; }
        public decimal MountInvestmentsInvestmentsPercentage { get; set; }

        public decimal crrInvestmentsValue { get; set; }
        public decimal totalInvestmentValue { get; set; }
        public int ClientCount { get; set; }
    }
}
