namespace MakeProfits.Backend.Models.AdvisorRequests
{
    public class Notification
    {
        public int FromID {  get; set; }
        public string FromUserName { get; set; }
        public int ToID { get; set; }
        public string ToUserName { get; set; }  
        public string Message {  get; set; }

    }
}
