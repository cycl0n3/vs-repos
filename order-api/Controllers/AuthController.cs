using Microsoft.AspNetCore.Mvc;

using order_api.Models;
using order_api.Services;

namespace order_api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UsersService _usersService;
        private readonly AuthService _authService;

        public AuthController(UsersService usersService, AuthService authService)
        {
            _usersService = usersService;
            _authService = authService;
        }

        // POST api/<AuthController>/login
        [HttpPost("login")]
        public async Task<ActionResult<User.LoginResponse>> Login([FromBody] User.LoginRequest request)
        {
            var user = await _usersService.FindByEmailAndPasswordAsync(request.Email, request.Password);

            if (user == null)
            {
                return NotFound();
            }

            var response = await _authService.Login(request);

            return Ok(response);
        }

        // POST api/<AuthController>/register
        [HttpPost("register")]
        public async Task<ActionResult<User>> Register([FromBody] User user)
        {
            var response = await _usersService.CreateAsync(user);

            return Ok(response);
        }
    }
}
