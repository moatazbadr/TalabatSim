using Microsoft.AspNetCore.Identity;
using Talabat.Domain.Entities.Identity;

namespace Talabat.Repository.Identity;

public static class AppIdentityDbContextSeed
{
    public static async Task SeedUserAsync(UserManager<AppUser> userManager)
    {
        if (!userManager.Users.Any())
        {
            var appUser = new AppUser()
            {
                Id = "19271e61-94ae-46b6-8a0e-cbe292887107", //id 
                DisplayName = "Moataz badr mohamed",
                Email = "zezobadr88@gmail.com",
                NormalizedEmail= "zezobadr88@gmail.com".ToUpper(),
                UserName = "Moataz.Badr",
                NormalizedUserName = "Moataz.Badr".ToUpper(),
                PhoneNumber="01097160693",
                EmailConfirmed=true,
                PhoneNumberConfirmed= true
                

            };
            await userManager.CreateAsync(appUser,"AMDTOP2001@s1");

        }
    }
}
