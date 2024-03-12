namespace MakeProfits.Backend.Models.Investments.Bonds
{
    public class BondInvestment
    {
        public int ClientID { get; set; }
        public int StratergyID { get; set; }
        public int BondID { get; set; }
        public string BondName { get; set; }
        public decimal BondPrice { get; set; }
        public int BondQuantity {  get; set; }
        public decimal InvestmentValue { get; set; }
    }
}
