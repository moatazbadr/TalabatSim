using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Talabat.Domain.Entities.Identity;
using Talabat.Domain.Services;

namespace Talabat.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;
        public TokenService(IConfiguration configuration)
        {
            _config = configuration;

        }


        public async Task<string> CreateToken(AppUser user,UserManager<AppUser> userManager)
        {
          
            var AuthClaim = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("DisplayName", user.DisplayName) //new Claim ("DisplayName", user.DisplayName)


            };
                var userRoles = await userManager.GetRolesAsync(user);
            foreach (var role in userRoles)
            {
                AuthClaim.Add(new Claim(ClaimTypes.Role, role));
            }   

            var Authkey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtSettings:SecretKey"]));

            var Token = new JwtSecurityToken(
                issuer:_config["JwtSettings:Issuer"]
                ,
                audience:_config["JwtSettings:Audience"],
                claims:AuthClaim,
               expires :DateTime.Now.AddDays(double.Parse(_config["JwtSettings:ExpirationInDays"])),
                signingCredentials:new SigningCredentials(Authkey,SecurityAlgorithms.HmacSha256)    
               );
            
            return new JwtSecurityTokenHandler().WriteToken(Token);





        }
    }
}
