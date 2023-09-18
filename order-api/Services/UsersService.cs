using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using order_api.Config;
using order_api.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace order_api.Services
{
    public class UsersService
    {
        private readonly OrderDatabaseConfig _orderDatabaseConfig;
        private readonly JwtConfig _jwtConfig;

        private readonly IMongoCollection<User> _users;

        public UsersService(IOptions<OrderDatabaseConfig> orderDatabaseConfig, IOptions<JwtConfig> jwtConfig)
        {
            _orderDatabaseConfig = orderDatabaseConfig.Value;
            _jwtConfig = jwtConfig.Value;

            var client = new MongoClient(_orderDatabaseConfig.ConnectionString);
            var database = client.GetDatabase(_orderDatabaseConfig.DatabaseName);

            _users = database.GetCollection<User>(_orderDatabaseConfig.UsersCollectionName);
        }

        public async Task<User> CreateAsync(User user)
        {
            user.Id = string.Empty;
            await _users.InsertOneAsync(user);
            return user;
        }

        public async Task<User> GetAsync(string id)
        {
            return await _users.Find(user => user.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<User>> GetAsync()
        {
            return await _users.Find(user => true).ToListAsync();
        }

        public async Task UpdateAsync(string id, User updatedUser)
        {
            await _users.ReplaceOneAsync(user => user.Id == id, updatedUser);
        }

        public async Task DeleteAsync(string id)
        {
            await _users.DeleteOneAsync(user => user.Id == id);
        }

        public async Task<User> FindByUsernameAsync(string username)
        {
            return await _users.Find(user => user.Username == username).FirstOrDefaultAsync();
        }

        public async Task<User> FindByEmailAsync(string email)
        {
            return await _users.Find(user => user.Email == email).FirstOrDefaultAsync();
        }

        public async Task<User.LoginResponse> Login(User.LoginRequest request)
        {
            // find user such that (username | email) and password match
            var user = await _users.Find(user => (user.Username == request.Username || user.Email == request.Username) && user.Password == request.Password).FirstOrDefaultAsync();

            if (user == null)
            {
                return new User.LoginResponse(string.Empty, string.Empty, string.Empty, string.Empty);
            }

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfig.Secret));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email),
            };

            var token = new JwtSecurityToken(
                _jwtConfig.Issuer,
                _jwtConfig.Audience,
                claims,
                expires: DateTime.Now.AddMinutes(3600),
                signingCredentials: credentials
            );

            return new User.LoginResponse(user.Id, user.Username, user.Email, new JwtSecurityTokenHandler().WriteToken(token));
        }
    }
}
