namespace MakeProfits.Backend.Models.Investments.Bonds
{
    public class BondInvestment
    {
        public Guid ClientID { get; set; }
        public Guid StratergyID { get; set; }
        public Guid BondID { get; set; }
        public string BondName { get; set; }
        public decimal BondPrice { get; set; }
        public int BondQuantity {  get; set; }
        public decimal InvestmentValue { get; set; }
    }
}
