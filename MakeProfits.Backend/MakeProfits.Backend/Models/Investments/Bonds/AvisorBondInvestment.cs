namespace MakeProfits.Backend.Models.Investments.Bonds
{
    public class AvisorBondInvestment : BondInvestment
    {
        public Guid AdvisorID { get; set; }

        public decimal InitialFund { get; set; }
    }
}
