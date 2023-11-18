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
    public class AuthService
    {
        private readonly JwtConfig _jwtConfig;

        private readonly IMongoCollection<User> _users;

        public AuthService(IOptions<OrderDatabaseConfig> orderDatabaseConfig, IOptions<JwtConfig> jwtConfig)
        {
            _jwtConfig = jwtConfig.Value;

            var client = new MongoClient(orderDatabaseConfig.Value.ConnectionString);
            var database = client.GetDatabase(orderDatabaseConfig.Value.DatabaseName);

            _users = database.GetCollection<User>(orderDatabaseConfig.Value.UsersCollectionName);
        }

        public async Task<User.LoginResponse> Login(User.LoginRequest request)
        {
            // find user such that email and password match
            var user = await _users.Find(user => user.Email == request.Email && user.Password == request.Password).FirstOrDefaultAsync();

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfig.Secret));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Email, user.Email),
            };

            var token = new JwtSecurityToken(
                _jwtConfig.Issuer,
                _jwtConfig.Audience,
                claims,
                expires: DateTime.Now.AddMinutes(3600),
                signingCredentials: credentials
            );

            return new User.LoginResponse(user.Id, user.Email, new JwtSecurityTokenHandler().WriteToken(token));
        }
    }
}
