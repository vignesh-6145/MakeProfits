using MakeProfits.Backend.Models;

namespace MakeProfits.Backend.Repository
{
    public interface IAdvisorDataAccess
    {
        IEnumerable<Advisor> GetAllAdvisors();
        Advisor GetAdvisor(int AdvisorID);
    }
}
