namespace MakeProfits.Backend.Models.Investments.MutualFunds
{
    public class MutualFundInvestment
    {
        public Guid ClientID { get; set; }
        public Guid StratergyID { get; set; }
        public Guid MutualFundID { get; set; }
        public String MutualFundName { get; set; }
        public decimal MutualFundPrice { get; set; }
        public int MutualFundQuantity { get; set; }
        public decimal InvestmentValue { get; set; }
    }
}
