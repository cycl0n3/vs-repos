using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace order_api.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = "password";
        public string Email { get; set; } = string.Empty;

        public User(string username, string email, string password)
        {
            Username = username;
            Password = password;
            Email = email;
        }

        public class LoginRequest
        {
            public string Username { get; set; } = string.Empty;
            public string Password { get; set; } = string.Empty;

            public LoginRequest(string username, string password)
            {
                Username = username;
                Password = password;
            }
        }

        public class LoginResponse
        {
            public string Id { get; set; } = string.Empty;
            public string Username { get; set; } = string.Empty;
            public string Email { get; set; } = string.Empty;
            public string Token { get; set; } = string.Empty;

            public LoginResponse(string id, string username, string email, string token)
            {
                Id = id;
                Username = username;
                Email = email;
                Token = token;
            }
        }
    }
}
