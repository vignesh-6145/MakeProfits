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

        public UserController(UserDataAccess userDataAccess)
        {
            _userDataAccess = userDataAccess;
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
        public ActionResult<User> Login(string Username, string Password)
        {
            return Ok(_userDataAccess.ValidateUser(Username, Password));
        }



    }
}
