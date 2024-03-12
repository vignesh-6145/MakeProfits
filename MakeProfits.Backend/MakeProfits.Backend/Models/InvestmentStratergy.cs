namespace MakeProfits.Backend.Models
{
    public class InvestmentStratergy
    {
        public Guid AdvisorID {  get; set; }
        public Guid ClientID { get; set; }
        public Guid StratergyID {  get; set; }
        public bool status {  get; set; }
        public decimal amount { get; set; }
    }
}
