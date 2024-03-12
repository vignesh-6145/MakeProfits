namespace MakeProfits.Backend.Models.Investments.MutualFunds
{
    public class MutualFundInvestment
    {
        public int ClientID { get; set; }
        public int StratergyID { get; set; }
        public int MutualFundID { get; set; }
        public String MutualFundName { get; set; }
        public decimal MutualFundPrice { get; set; }
        public int MutualFundQuantity { get; set; }
        public decimal InvestmentValue { get; set; }
    }
}
