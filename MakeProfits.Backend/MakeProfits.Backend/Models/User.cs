using MakeProfits.Backend.Models;

namespace MakeProfits.Models
{
    public class User : AbstractUser
    {

        public int UserID { get; set; }

        public string Password { get; set; }
        public int RoleID { get; set; }
        public int AgentID { get; set; }
        public int AdvisorID { get; set; }
        public bool IsActive { get; set; }

    }
}
