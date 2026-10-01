using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Talabat.Domain.Entities.Identity;

namespace Talabat.APIs.Extensions;

public static class UserManagerExtension
{
    //include user Address
    public static async Task<AppUser> FindByEmailWithAddressAsync(this UserManager<AppUser> userManager, string email)
    {
        return await userManager.Users.Include(u => u.Address).FirstOrDefaultAsync(u => u.Email == email);
    }
}
