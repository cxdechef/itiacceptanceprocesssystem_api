using ITIAcceptanceProcessSystem.Constants;
using ITIAcceptanceProcessSystem.Models;
using ITIAcceptanceProcessSystem.UnitOfWork;
using Microsoft.AspNetCore.Identity;

namespace ITIAcceptanceProcessSystem.Data
{
    public class SeedData
    {
        private readonly ILogger<SeedData> _logger;

        public SeedData(ILogger<SeedData> logger)
        {
            _logger = logger;
        }

        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
            var logger = serviceProvider.GetRequiredService<ILogger<SeedData>>();
            var unitOfWork = serviceProvider.GetRequiredService<IUnitOfWork>();

            await SeedRoles(roleManager, logger);

            await SeedAdminUser(userManager, unitOfWork, logger);
        }

        public static async Task SeedRoles(RoleManager<IdentityRole> roleManager, ILogger logger)
        {
            string[] roleNames = { UserRoles.Candidate, UserRoles.Instructor, UserRoles.Admin };
            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                    logger.LogInformation($"Role '{roleName}' created.");
                }
            }
        }

        public static async Task SeedAdminUser(UserManager<User> userManager, IUnitOfWork unitOfWork, ILogger logger)
        {
            var adminEmail = "admin@admin.admin"; // Change as needed
            var adminPassword = "Admin@123"; // Change as needed

            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                adminUser = new User { UserName = adminEmail, Email = adminEmail };
                var result = await userManager.CreateAsync(adminUser, adminPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, UserRoles.Admin);
                    logger.LogInformation($"Admin user '{adminEmail}' created and assigned to the Admin role.");

                    var adminRecord = new Admin
                    {
                        UserId = adminUser.Id,
                        FullName = "AdminUser" // Change as needed
                    };

                    await unitOfWork.Admins.AddAsync(adminRecord);
                    await unitOfWork.SaveAsync();
                    logger.LogInformation($"Admin record for '{adminEmail}' added to the Admins table.");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        logger.LogError($"Error creating admin user: {error.Description}");
                    }
                }
            }
            else
            {
                logger.LogInformation("Admin user already exists.");
            }
        }
    }
}
