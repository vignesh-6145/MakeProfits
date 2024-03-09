using System.ComponentModel.DataAnnotations;

namespace MakeProfits.Backend.Models.AdvisorRequests
{
    public class AdvisoryRequest
    {
        public int ClientID { get; set; }
        public int AdvisorID { get; set; }
        public int StratergyID { get; set; } = 1;
        public string RequestBY { get; set; } // A - Request made by Advisor, C - Reqquest Made by Client

        [MaxLength(50)]
        public string Message { get; set; }
    }
}
