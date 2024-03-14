namespace MakeProfits.Backend.Models
{
    public class Strategy
    {
            public Guid StrategyID { get; set; }
            public Guid AdvisorID { get; set; }
            public Guid StockID { get; set; }
            public Guid MFID { get; set; }
            public Guid BondsID { get; set; }
            public double StockPercentage { get; set; }
            public double MFPercentage { get; set; }
            public double BondsPercentage { get; set; }
        

    }
}
