using System.ComponentModel.DataAnnotations;

namespace MakeProfits.Backend.Models.AdvisorRequests
{
    public class AdvisoryRequest
    {
        public Guid ClientID { get; set; }
        public Guid AdvisorID { get; set; }
        public Guid StratergyID { get; set; } 
        public string RequestBY { get; set; } // A - Request made by Advisor, C - Reqquest Made by Client

        [MaxLength(50)]
        public string Message { get; set; }
    }
}
