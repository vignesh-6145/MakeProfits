using MakeProfits.Backend.Models;
using MakeProfits.Backend.Repository;
using MakeProfits.Models;
using MakeProfits.Repository;
using Microsoft.AspNetCore.Mvc;

namespace MakeProfits.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {


        private readonly IUserDataAccess _userDataAccess;
        private readonly ILogger<UserController> _logger;
        public UserController(IUserDataAccess userDataAccess, ILogger<UserController> logger)
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



    }
}
