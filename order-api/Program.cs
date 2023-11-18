using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

using order_api.Config;
using order_api.Services;

using System.Text;

namespace order_api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

            // Add services to the container.
            builder.Services.Configure<OrderDatabaseConfig>(builder.Configuration.GetSection(nameof(OrderDatabaseConfig)));
            builder.Services.Configure<JwtConfig>(builder.Configuration.GetSection(nameof(JwtConfig)));

            builder.Services.AddSingleton<UsersService>();
            builder.Services.AddSingleton<AuthService>();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:5173")
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    }
                );
            });

            var key = builder.Configuration["JwtConfig:Secret"];
            if (string.IsNullOrEmpty(key))
            {
                throw new Exception("JwtConfig:Secret is missing in appsettings.json");
            }

            var issuer = builder.Configuration["JwtConfig:Issuer"];
            if (string.IsNullOrEmpty(issuer))
            {
                throw new Exception("JwtConfig:Issuer is missing in appsettings.json");
            }

            var audience = builder.Configuration["JwtConfig:Audience"];
            if (string.IsNullOrEmpty(audience))
            {
                throw new Exception("JwtConfig:Audience is missing in appsettings.json");
            }

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,

                        ValidIssuer = issuer,
                        ValidAudience = audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
                    };
                });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors(MyAllowSpecificOrigins);

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}