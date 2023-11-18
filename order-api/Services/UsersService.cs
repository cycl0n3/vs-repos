using Microsoft.Extensions.Options;

using MongoDB.Driver;

using order_api.Config;
using order_api.Models;

namespace order_api.Services
{
    public class UsersService
    {
        private readonly JwtConfig _jwtConfig;

        private readonly IMongoCollection<User> _users;

        public UsersService(IOptions<OrderDatabaseConfig> orderDatabaseConfig, IOptions<JwtConfig> jwtConfig)
        {
            _jwtConfig = jwtConfig.Value;

            var client = new MongoClient(orderDatabaseConfig.Value.ConnectionString);
            var database = client.GetDatabase(orderDatabaseConfig.Value.DatabaseName);

            _users = database.GetCollection<User>(orderDatabaseConfig.Value.UsersCollectionName);
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

        public async Task<User> FindByEmailAsync(string email)
        {
            return await _users.Find(user => user.Email == email).FirstOrDefaultAsync();
        }

        public async Task<User> FindByEmailAndPasswordAsync(string email, string password)
        {
            return await _users.Find(user => user.Email == email && user.Password == password).FirstOrDefaultAsync();
        }
    }
}
