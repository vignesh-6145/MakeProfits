using MakeProfits.Backend.Models;

namespace MakeProfits.Models
{
    public class User : AbstractUser
    {

        public Guid UserID { get; set; }

        public string Password { get; set; }
        public Guid RoleID { get; set; }
        public Guid AgentID { get; set; }
        public Guid AdvisorID { get; set; }
        public bool IsActive { get; set; }

    }
}
