using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace LibraryManagementAPI.Extensions
{
    public static class SwaggerConfiguration
    {
        public static void AddSwaggerConfiguration(this IServiceCollection services, IConfiguration config)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.Audience = config["JWT:Audience"];
                options.TokenValidationParameters = new TokenValidationParameters
                {       

                    ValidateIssuerSigningKey = config.GetValue<bool>("JWT:ValidateSigningKey"),// TokenConstants.ValidateSigningKey,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.GetValue<string>("JWT:SecurityKey"))),
                    ValidateIssuer = config.GetValue<bool>("JWT:ValidateIssuer"),
                    ValidIssuer = config.GetValue<string>("JWT:Issuer"),
                    ValidateAudience = config.GetValue<bool>("JWT:ValidateAudience"),
                    ValidAudience = config.GetValue<string>("JWT:Audience"),
                    ValidateLifetime = config.GetValue<bool>("JWT:ValidateLifeTime"), //validate the expiration and not before values in the token
                    ClockSkew = TimeSpan.FromMinutes(config.GetValue<int>("JWT:DateToleranceMinutes")) //5 minute tolerance for the expiration date
                };
            });
        }
        public static void AddSwaggerGen(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "LibraryManagement", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme."
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme, 
                                Id = "Bearer"
                            }
                        },
                        new string[] {}

                    }
                });
            });
        }
    }
}
