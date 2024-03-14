using MakeProfits.Backend.Models;
using MakeProfits.Backend.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MakeProfits.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StrategyController : ControllerBase
    {
        private readonly IStrategyDataAccess _strategyDataAccess;

        public StrategyController(IStrategyDataAccess strategyDataAccess)
        {
            _strategyDataAccess = strategyDataAccess;
        }
        [HttpPost("Addstrategy")]
        public ActionResult<Strategy> AddStrat(Strategy strategy)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            return Ok(_strategyDataAccess.addStrat(strategy));
        }
        [HttpPost("Showstrategy")]
        public ActionResult<List<Strategy>> showStrat(Guid advisorId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            return Ok(_strategyDataAccess.showStrat(advisorId));
        }
        [HttpPost("Deletestratagy")]
        public ActionResult<int> deleteStrat(Guid strategyId)
        {

            _strategyDataAccess.deletestrat(strategyId);

            return Ok(0);
        }
        [HttpPost("getivestname")]
        public ActionResult<string> getinvestname(string table, Guid id)
        {



            return Ok(_strategyDataAccess.getinvestname(table, id));
        }
        [HttpPost("checkfirsttime")]

        public ActionResult<bool> checkFirsttime(Guid clientId)
        {
            return Ok(_strategyDataAccess.CheckFirstTime(clientId));
        }
        [HttpPost("subscription")]

        public ActionResult<int> subscription(Guid clientId)
        {
            _strategyDataAccess.subscription(clientId);
            return Ok(1);
        }

    }
}
