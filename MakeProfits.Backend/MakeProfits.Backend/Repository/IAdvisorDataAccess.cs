using MakeProfits.Backend.Models;
using MakeProfits.Backend.Models.AdvisorRequests;

namespace MakeProfits.Backend.Repository
{
    public interface IAdvisorDataAccess
    {
        IEnumerable<Advisor> GetAllAdvisors();
        Advisor GetAdvisor(Guid AdvisorID);

        IEnumerable<Advisor> GetClientAdvisors(Guid ClinetID);
        IEnumerable<AbstractUser> GetAdvisorClients(Guid AdvisorID);

        bool AddCient(AdvisoryRequest advisoryRequest);
    }
}
