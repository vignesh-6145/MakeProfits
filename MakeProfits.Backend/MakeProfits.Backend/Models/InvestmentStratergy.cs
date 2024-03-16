namespace MakeProfits.Backend.Models
{
    public class InvestmentStratergy
    {
        public Guid AdvisorID {  get; set; }
        public Guid ClientID { get; set; }
        public Guid StratergyID {  get; set; }
        public bool status {  get; set; }
        public int StockID { get; set; }
        public int MFID { get; set; }
        public int BondsID { get; set; }
        public double StockPercentage { get; set; }
        public double MFPercentage { get; set; }
        public double BondsPercentage { get; set; }
        public decimal amount { get; set; }
    }
}
