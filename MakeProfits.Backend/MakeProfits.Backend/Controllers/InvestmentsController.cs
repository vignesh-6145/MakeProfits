using MakeProfits.Backend.Models;
using MakeProfits.Backend.Utillity;
using Microsoft.AspNetCore.Mvc;

namespace MakeProfits.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvestmentsController : Controller
    {
        private readonly IInvestmentsUtility _APIUtility;
        private readonly ILogger<InvestmentsController> _logger;
        public InvestmentsController(ILogger<InvestmentsController> logger,IInvestmentsUtility APIUtility)
        {
            this._logger = logger;
            this._APIUtility = APIUtility;
        }
        
        [HttpGet("getAllInvestments/{tickSymbol}")]
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
    }
}
