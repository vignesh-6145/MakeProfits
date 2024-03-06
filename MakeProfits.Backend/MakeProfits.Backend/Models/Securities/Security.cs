namespace MakeProfits.Backend.Models.Securities
{
    public class Security : AbstractSecurity
    {
        public int SecurityID { get; set; }
        public DateTime UpdatedOn { get; set; } = DateTime.Now;
    }
}
