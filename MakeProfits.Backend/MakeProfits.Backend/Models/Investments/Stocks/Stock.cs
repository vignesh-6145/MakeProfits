namespace MakeProfits.Backend.Models.Investments.Stocks
{
    public class Stock : AbstractStock
    {
        public Guid SecurityID { get; set; }
        public DateTime UpdatedOn { get; set; } = DateTime.Now;
    }
}
