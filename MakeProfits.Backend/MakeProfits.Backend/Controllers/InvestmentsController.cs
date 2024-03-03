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
        public async Task<AbstractInvestmentInfo> GetInvestmentBrief(string tickSymbol)
        {
            var resp = await _APIUtility.GetInvestmentInfoAsync(tickSymbol);
            return resp;
           
        }
    }
}
