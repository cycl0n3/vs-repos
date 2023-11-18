using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using order_api.Models;
using order_api.Services;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace order_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UsersService _usersService;

        private readonly AuthService _authService;

        private readonly ILogger<UserController> _logger;

        public UserController(UsersService usersService, AuthService authService, ILogger<UserController> logger)
        {
            _usersService = usersService;
            _authService = authService;
            _logger = logger;
        }

        [HttpGet("all")]
        [Authorize]
        public async Task<ActionResult<Dictionary<string, object>>> Get()
        {
            var user = HttpContext.User;
            var email = user.FindFirst(ClaimTypes.Email)?.Value;

            var users = await _usersService.GetAsync();

            var result = new Dictionary<string, object?>
            {
                { "users", users },
                { "me", email }
            };

            return Ok(result);
        }

        // GET: api/<UsersController>/{id}
        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<User>> Get([FromRoute] string id)
        {
            var user = await _usersService.GetAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // POST api/<UsersController>
        [HttpPost]
        public async Task<ActionResult<User>> Post([FromBody] User user)
        {
            var createdUser = await _usersService.CreateAsync(user);

            return CreatedAtAction(nameof(Get), new { id = createdUser.Id }, createdUser);
        }

        // Update api/<UsersController>/{id}
        [HttpPut("{id:length(24)}")]
        public async Task<ActionResult> Update([FromRoute] string id, [FromBody] User updatedUser)
        {
            var user = await _usersService.GetAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            updatedUser.Id = id;

            await _usersService.UpdateAsync(id, updatedUser);

            return NoContent();
        }

        // DELETE api/<UsersController>/{id}
        [HttpDelete("{id:length(24)}")]
        public async Task<ActionResult> Delete([FromRoute] string id)
        {
            var user = await _usersService.GetAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            await _usersService.DeleteAsync(id);

            return NoContent();
        }
    }
}
