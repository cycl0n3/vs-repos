using Microsoft.AspNetCore.Mvc;
using order_api.Models;
using order_api.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace order_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UsersService _usersService;

        private readonly ILogger<UsersController> _logger;

        public UsersController(UsersService usersService, ILogger<UsersController> logger)
        {
            _usersService = usersService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> Get()
        {
            return Ok(await _usersService.GetAsync());
        }

        // GET: api/<UsersController>
        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<User>> Get(string id)
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
        public async Task<ActionResult<User>> Post(User user)
        {
            var createdUser = await _usersService.CreateAsync(user);

            return CreatedAtAction(nameof(Get), new { id = createdUser.Id }, createdUser);
        }

        // Update api/<UsersController>/{id}
        [HttpPut("{id:length(24)}")]
        public async Task<ActionResult> Update(string id, User updatedUser)
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
        public async Task<ActionResult> Delete(string id)
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
