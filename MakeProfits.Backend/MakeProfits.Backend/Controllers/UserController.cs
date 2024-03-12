using MakeProfits.Backend.Models.AdvisorRequests;
using MakeProfits.Models;
using MakeProfits.Repository;
using Microsoft.AspNetCore.Mvc;

namespace MakeProfits.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {


        private readonly UserDataAccess _userDataAccess;
        private readonly ILogger<UserController> _logger;
        public UserController(UserDataAccess userDataAccess, ILogger<UserController> logger)
        {
            _userDataAccess = userDataAccess;
            _logger = logger;
        }

        //Working
        // POST: api/User/Register
        [HttpPost("Register")]
        public ActionResult<User> Register(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _userDataAccess.RegisterUser(user);
            return Ok(user);
        }
        [HttpPost("Login")]
        public ActionResult Login(string Username, string Password)
        {
            if(_userDataAccess.ValidateUser(Username, Password))
            {
                _logger.LogInformation("User authenticated successfully");
                return Ok(_userDataAccess.getUserByUserName(Username));
            }
            else
            {
                _logger.LogInformation("Unable to Authenticate user {UserName}",Username);
                return BadRequest("Wrong Credentials");
            }
        }

        [HttpPost("RequestAdvisor")]
        public ActionResult ChooseAdvisor([FromBody] AdvisoryRequest clientRequest)
        {
            //As of now noe approval is required, but in future it was required, check stored procedure for logic
            /*
             CREATE PROCEDURE client_request_advisor 
	            @clientID INTEGER
	            ,@advisorID INTEGER
	            ,@stratergyID INTEGER = 1
	            ,@Message NVARCHAR(50) = 'Request for advisory'
            AS
            BEGIN
	            INSERT INTO client_advisor_funds(clientid,advisorid,strategyID) VALUES (@clientID,@advisorID,@stratergyID);
	            INSERT INTO advisory_requests(clientID,advisorID,stratergyID,request_by) VALUES(@clientID,@advisorID,@stratergyID,'C');
	            INSERT INTO Notifications(fromID,toID,message) VALUES (@clientID,@advisorID,@Message);

	            SELECT @@ROWCOUNT;
            END*/
            if (_userDataAccess.RequestAdvisory(clientRequest))
            {
                return Ok("Reuest posted to Adviosr");
            }
            else
            {
                return BadRequest("Failed to request");
            }
        }

        [HttpGet("ReadNotifications/{UserID}")]
        public ActionResult ReadNotifications(Guid UserID)
        {
            _logger.LogInformation("Initiating the process of retreiving requests of User {UserID}",UserID);
            var notifications = _userDataAccess.ReadNotifications(UserID);
            if (notifications != null)
            {
                if (notifications.Count() == 0)
                {
                    return Ok("No Notifications");
                }
                else
                {
                    return Ok(notifications);
                }                 
            }
            
            else
            {
                return BadRequest("No  ");
            }

        }

    }
}
