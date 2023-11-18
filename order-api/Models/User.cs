using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace order_api.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = "password";

        public string Roles { get; set; } = string.Empty;

        public User(string email, string password)
        {
            Email = email;
            Password = password;
        }

        public class LoginRequest
        {
            public string Email { get; set; } = string.Empty;
            public string Password { get; set; } = string.Empty;

            public LoginRequest(string email, string password)
            {
                Email = email;
                Password = password;
            }
        }

        public class LoginResponse
        {
            public string Id { get; set; } = string.Empty;
            public string Email { get; set; } = string.Empty;
            public string Token { get; set; } = string.Empty;

            public LoginResponse(string id, string email, string token)
            {
                Id = id;
                Email = email;
                Token = token;
            }
        }
    }
}
