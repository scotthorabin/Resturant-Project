using Microsoft.AspNetCore.Identity;

namespace resturant.Data
{
    public class IdentitySeedData
    {
        // new method that will have database context, user manager and role manager
        public static async Task Initialize(resturantContext context,
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            // Checks to see if database has been created
            context.Database.EnsureCreated();
            // Declare variable for role names and password
            string adminRole = "Admin";
            string memberRole = "Member";
            string password4all = "P@55word";

            // If roles are not found, then create them
            if (await roleManager.FindByNameAsync(adminRole) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(adminRole));
            }

            if (await roleManager.FindByNameAsync(memberRole) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(memberRole));
            }

            // if admin user does not exist then create and assign

            if (await userManager.FindByNameAsync("admin@uoc.ac.uk") == null)
            {
                var user = new IdentityUser
                {
                    UserName = "admin@uoc.ac.uk",
                    Email = "admin@uoc.ac.uk",
                    PhoneNumber = "06124 648200"
                };

                var result = await userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    await userManager.AddPasswordAsync(user, password4all);
                    await userManager.AddToRoleAsync(user, adminRole);
                }
                
            }
            // if member user does not exist then create and assign
            if (await userManager.FindByNameAsync("member@uoc.ac.uk") == null)
            {
                var user = new IdentityUser
                {
                    UserName = "member@uoc.ac.uk",
                    Email = "member@uoc.ac.uk",
                    PhoneNumber = "06124 648200"
                };

                var result = await userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    await userManager.AddPasswordAsync(user, password4all);
                    await userManager.AddToRoleAsync(user, memberRole);
                }

            }
        }

    }
}
