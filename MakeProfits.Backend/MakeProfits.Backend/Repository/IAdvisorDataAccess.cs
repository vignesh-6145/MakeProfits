using MakeProfits.Backend.Models;
using MakeProfits.Backend.Models.AdvisorRequests;

namespace MakeProfits.Backend.Repository
{
    public interface IAdvisorDataAccess
    {
        IEnumerable<Advisor> GetAllAdvisors();
        Advisor GetAdvisor(int AdvisorID);

        IEnumerable<Advisor> GetClientAdvisors(int ClinetID);
        IEnumerable<AbstractUser> GetAdvisorClients(int AdvisorID);

        bool AddCient(AdvisoryRequest advisoryRequest);
    }
}
