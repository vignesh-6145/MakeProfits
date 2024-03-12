namespace MakeProfits.Backend.Models.Investments.MutualFunds
{
    public class MutualFund
    {
        //beta > 1 more volatile than benchmark
        //beta < 1 less volatile than benchmark
        public Guid MF_ID {  get; set; }
        public string ISIN { get; set; }
        public string symbol { get; set; }
        public decimal Beta { get; set; } // volaility
        public string MF_Name { get; set; }
        public bool IsOpen { get; set; }
        public decimal MinInvestment { get; set; }
        public decimal ExpenseRatio { get; set; }

    }
}
