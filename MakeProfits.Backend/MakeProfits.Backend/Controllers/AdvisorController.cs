using MakeProfits.Backend.Models.AdvisorRequests;
using MakeProfits.Backend.Repository;
using Microsoft.AspNetCore.Mvc;

namespace MakeProfits.Backend.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class AdvisorController : Controller
    {

        private readonly IAdvisorDataAccess _dataAccess;
        private readonly ILogger<AdvisorController> _logger;
        public AdvisorController(IAdvisorDataAccess dataAccess, ILogger<AdvisorController> logger) { 
            this._dataAccess = dataAccess;
            _logger = logger;
        }  
        [HttpGet("getAllAdvisors")]
        public IActionResult GetAllAdvisors()
        {
            _logger.LogInformation("Request to retrieve all the advisors received");
            return Ok(_dataAccess.GetAllAdvisors());
            // SP - GetAdvisorInfo
        }

        [HttpGet("{AdvisorID}")]
        public IActionResult GetAdvisor(int AdvisorID)
        {
            _logger.LogInformation("Request to retrieve all the advisors received");
            var advisor = _dataAccess.GetAdvisor(AdvisorID);
            if(advisor != null)
            {
                return Ok(advisor);
            }
            else
            {
                return BadRequest($"No Adviosrs for client with ID {AdvisorID}");
            }
            // SP - GetAdvisorInfo
        }

        [HttpGet("Client/{ClientID}")]
        public IActionResult getUseAdvisors(int ClientID)
        {
            _logger.LogInformation("Request to retrieve all the advisors received");
            var advisor = _dataAccess.GetClientAdvisors(ClientID);
            if (advisor != null)
            {
                return Ok(advisor);
            }
            else
            {
                return BadRequest($"No Adviosr with ID {ClientID}");
            }
            // SP - GetAdvisorInfo
        }

        [HttpGet("{AdvisorID}/Clients")]
        public IActionResult GetAdvisorClients(int AdvisorID)
        {
            var clients = _dataAccess.GetAdvisorClients(AdvisorID);
            if (clients != null)
                return Ok(clients);
            else
                return BadRequest($"No Clinets for Advisor with {AdvisorID}");
        }

        [HttpPost("AddClient")]
        public IActionResult AddClient([FromBody] AdvisoryRequest clientRequest)
        {
            /*
             * CREATE PROCEDURE adviser_request_client 
	            @clientID INTEGER
	            ,@advisorID INTEGER
	            ,@stratergyID INTEGER = 1
	            ,@Message NVARCHAR(50) = 'Advisor added you as a client'
            AS
            BEGIN
	            INSERT INTO client_advisor_funds(clientid,advisorid,strategyID) VALUES (@clientID,@advisorID,@stratergyID);
	            INSERT INTO advisory_requests(clientID,advisorID,stratergyID,request_by) VALUES(@clientID,@advisorID,@stratergyID,'A');
	            INSERT INTO Notifications(fromID,toID,message) VALUES (@clientID,@advisorID,@Message);

	            SELECT @@ROWCOUNT;
            END
             */
            //As of now noe approval is required, but in future it was required, check stored procedure for logic
            if (_dataAccess.AddCient(clientRequest))
            {
                return Ok("Requested user to be your client");
            }
            else
            {
                return BadRequest("Something went wrong");
            }
        }
    }
}
