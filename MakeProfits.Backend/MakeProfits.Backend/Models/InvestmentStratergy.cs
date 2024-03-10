namespace MakeProfits.Backend.Models
{
    public class InvestmentStratergy
    {
        public int AdvisorID {  get; set; }
        public int ClientID { get; set; }
        public int StratergyID {  get; set; }
        public bool status {  get; set; }
        public int amount { get; set; }
    }
}
