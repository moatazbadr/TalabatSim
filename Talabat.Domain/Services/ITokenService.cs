using Microsoft.AspNetCore.Identity;
using Talabat.Domain.Entities.Identity;

namespace Talabat.Domain.Services;

public interface ITokenService
{
    Task<string> CreateToken(AppUser user,UserManager<AppUser> userManager);
}
