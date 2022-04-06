using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INTEX2.Models
{
    public static class IdentitySeedData
    {
        private const string adminUser = "AdminUser";
        private const string adminPassword = "k7T#2n&9C0wdm!J";

        public static async void EnsurePopulated(IApplicationBuilder app)
        {
            AppIdentityDBContext context = app.ApplicationServices
                .CreateScope().ServiceProvider
                .GetRequiredService<AppIdentityDBContext>();

            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }

            UserManager<IdentityUser> userManager = app.ApplicationServices
                .CreateScope().ServiceProvider
                .GetRequiredService<UserManager<IdentityUser>>();

            IdentityUser user = await userManager.FindByIdAsync(adminUser);

            if (user == null)
            {
                user = new IdentityUser(adminUser);

                user.Email = "admin@yeet.u.dot.com";
                user.PhoneNumber = "385-123-9338";

                await userManager.CreateAsync(user, adminPassword);
            }

        }
    }
}
