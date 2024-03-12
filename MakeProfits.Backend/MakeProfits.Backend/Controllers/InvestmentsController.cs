using MakeProfits.Backend.Models;
using MakeProfits.Backend.Repository;
using MakeProfits.Backend.Utillity;
using Microsoft.AspNetCore.Mvc;

namespace MakeProfits.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvestmentsController : Controller
    {
        private readonly IInvestmentsUtility _APIUtility;
        private readonly IInvestmentDataAccess _dataAccess;
        private readonly ILogger<InvestmentsController> _logger;
        public InvestmentsController(ILogger<InvestmentsController> logger,IInvestmentsUtility APIUtility,IInvestmentDataAccess dataAccess)
        {
            this._logger = logger;
            this._APIUtility = APIUtility;
            this._dataAccess = dataAccess;
        }
        
        [HttpGet("getInvestmentInfo/{tickSymbol}")]
        public async Task<ActionResult> GetInvestmentBrief(string tickSymbol)
        {
            _logger.LogInformation("Initiating the process of retrieving data for {tickSymbol}", tickSymbol);
            var resp = await _APIUtility.GetInvestmentInfoAsync(tickSymbol);
            if(resp is null)
            {
                return NotFound($"Unable to Retrieve Information for {tickSymbol}");
            }
            return Ok(resp);

        }
        [HttpPost("optinstratergy")]
        public ActionResult OptInStratergy([FromBody]InvestmentStratergy investmentStratergy)
        {
            if (_dataAccess.OptinStratergy(investmentStratergy))
            {
                return Ok("Invested  in stratergy");
            }
            else
            {
                return BadRequest("Something went wrong");
            }
        }
        [HttpPost("optoutstratergy")]
        public ActionResult OptOutStratergy([FromBody] InvestmentStratergy investmentStratergy)
        {
            if (_dataAccess.OptoutStratergy(investmentStratergy))
            {
                return Ok("Transaction completed successfully");
            }
            else
            {
                return BadRequest("Something went wrong");
            }
        }
        [HttpGet("User/{UserID}/portfolio")]
        public ActionResult GetUserPortfolio(Guid UserID,string InvestmentType = "All")
        {
            _logger.LogInformation("Initiaitng the process of Retrievng user {Type} Portfolio of client",InvestmentType,UserID);
            var userPortfolio = _dataAccess.GetUserProtfolio(UserID,InvestmentType);
            if(userPortfolio == null)
            {
                return BadRequest("User Portfolio is Empty");
            }
            return Ok(userPortfolio);
        }
        [HttpGet("Advisor/{AdvisorID}/portfolio")]
        public ActionResult GetAdvisorPortfolio(Guid AdvisorID,string InvestmentType = "All")
        {
            _logger.LogInformation("Initiating the process of Retrieving Advisor portfolio");
            return Ok(_dataAccess.GetAdvisorPortfolio(AdvisorID, InvestmentType));
        }
    }


}
