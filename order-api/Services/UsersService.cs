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
            user.Id = null;
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

        public async Task<User> Login(User.LoginRequest request)
        {
            // find user such that (username | email) and password match
            return await _users.Find(user => (user.Username == request.Username || user.Email == request.Username) && user.Password == request.Password).FirstOrDefaultAsync();
        }
    }
}
