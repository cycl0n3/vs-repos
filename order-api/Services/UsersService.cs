using Microsoft.Extensions.Options;
using MongoDB.Driver;
using order_api.Config;
using order_api.Models;

namespace order_api.Services
{
    public class UsersService
    {
        private readonly OrderDatabaseConfig _orderDatabaseConfig;

        private readonly IMongoCollection<User> _users;

        public UsersService(IOptions<OrderDatabaseConfig> options)
        {
            _orderDatabaseConfig = options.Value;

            var client = new MongoClient(_orderDatabaseConfig.ConnectionString);
            var database = client.GetDatabase(_orderDatabaseConfig.DatabaseName);

            _users = database.GetCollection<User>(_orderDatabaseConfig.UsersCollectionName);
        }

        public async Task<User> CreateAsync(User user)
        {
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
    }
}
