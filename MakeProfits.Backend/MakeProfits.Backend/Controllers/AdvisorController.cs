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
    }
}
