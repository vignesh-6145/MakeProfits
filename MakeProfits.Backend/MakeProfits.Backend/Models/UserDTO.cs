namespace MakeProfits.Backend.Models
{
    public class UserDTO:AbstractUser
    {
        public string Role { get; set; }
        public string AgentName { get; set; }
        public string AdvisorName { get; set; }
    }
}
