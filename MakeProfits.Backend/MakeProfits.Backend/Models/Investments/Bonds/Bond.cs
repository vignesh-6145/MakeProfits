namespace MakeProfits.Backend.Models.Investments.Bonds
{
    public class Bond
    {
        public int BondID { get; set; }
        public string ISIN { get; set; }
        public string BondName { get; set; }
        public string BondType { get; set; }
        public decimal Price { get; set; }
        public decimal OfferingPrice { get; set; }
        public decimal Coupon { get; set; } // interest 
        public DateTime MaturityDate { get; set; }

        public int paymentFrequence { get; set; } // in months

    }
}
