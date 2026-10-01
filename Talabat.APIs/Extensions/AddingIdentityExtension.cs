using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Talabat.Domain.Entities.Identity;
using Talabat.Domain.Services;
using Talabat.Repository.Identity;
using Talabat.Services;

namespace Talabat.APIs.Extensions
{
    public static class AddingIdentityExtension
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ITokenService, TokenService>();
             
            //add signInManager to the services collection
         // the correct way to add signInManager ==>   
            services.AddIdentity<AppUser, IdentityRole>()
                .AddSignInManager<SignInManager<AppUser>>()
                .AddEntityFrameworkStores<AppIdentityDbContext>()
                ;

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options=> 
                {
                    options.TokenValidationParameters =
                    new TokenValidationParameters()
                    {
                        ValidateIssuer=true,
                        ValidIssuer=configuration["JwtSettings:Issuer"],
                        ValidateAudience=true,
                        ValidAudience=configuration["JwtSettings:Audience"],
                        ValidateLifetime=true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:SecretKey"]))
                    };
                
                
                } 
                
                ) 
                ;

            return services;
        }
    }
}
