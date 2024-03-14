namespace MakeProfits.Backend.Models.Investments.MutualFunds
{
    public class AdvisorMutualFundInvestment : MutualFundInvestment
    {
        public Guid AdvisorID {  get; set; }
        public decimal InitialFund { get; set; }
    }
}
