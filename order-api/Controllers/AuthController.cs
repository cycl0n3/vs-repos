using Microsoft.AspNetCore.Mvc;
using order_api.Models;
using order_api.Services;

namespace order_api.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly UsersService _usersService;

        public AuthController(UsersService usersService)
        {
            _usersService = usersService;
        }

        // POST api/<AuthController>
        [HttpPost("/login")]
        public async Task<ActionResult<User>> Login([FromBody] User.LoginRequest request)
        {
            var foundUser = await _usersService.Login(request);

            if (foundUser == null)
            {
                return NotFound();
            }

            return Ok(foundUser);
        }

        // POST api/<AuthController>
        [HttpPost("/register")]
        public async Task<ActionResult<User>> Register([FromBody] User user)
        {
            var foundUserByUsername = await _usersService.FindByUsernameAsync(user.Username);
            var foundUserByEmail = await _usersService.FindByEmailAsync(user.Email);

            if (foundUserByUsername != null || foundUserByEmail != null)
            {
                return Conflict();
            }

            var createdUser = await _usersService.CreateAsync(user);

            return CreatedAtAction(nameof(Login), new { id = createdUser.Id }, createdUser);
        }
    }
}
