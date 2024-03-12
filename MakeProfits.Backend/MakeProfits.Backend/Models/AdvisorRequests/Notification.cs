namespace MakeProfits.Backend.Models.AdvisorRequests
{
    public class Notification
    {
        public Guid FromID {  get; set; }
        public string FromUserName { get; set; }
        public Guid ToID { get; set; }
        public string ToUserName { get; set; }  
        public string Message {  get; set; }

    }
}
