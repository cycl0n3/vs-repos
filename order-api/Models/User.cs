using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace order_api.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = "password";

        public User(string username, string email, string password)
        {
            Username = username;
            Email = email;
            Password = password;
        }

        public class LoginRequest
        {
            public string Username { get; set; } = string.Empty;
            public string Password { get; set; } = string.Empty;
        }
    }
}
