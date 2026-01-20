using cctvtemplate.Models;
using cctvtemplate.ViewModels.AuthViewModels;
using Microsoft.AspNetCore.Identity;

namespace cctvtemplate.Helpers
{
    public class DbContextInitalizer
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _config;
        private readonly AdminVm _admin;

        public DbContextInitalizer(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration config)
        {
            _userManager = userManager;
            _roleManager  = roleManager;
            _config = config;
            _admin = _config.GetSection("AdminSettings").Get<AdminVm>() ?? new();
        }


        public async Task DbContextInit()
        {
            await CreateRoles();
            await CreateAdmin();
        }




        private async Task CreateRoles()
        {
            await _roleManager.CreateAsync(new() { Name = "Admin" });
            await _roleManager.CreateAsync(new() { Name = "Moderator" });
            await _roleManager.CreateAsync(new() { Name = "Member" });
            await _roleManager.CreateAsync(new() { Name = "ElsenMellim" });
        }


        private async Task CreateAdmin()
        {
            AppUser admin = new()
            {
                UserName = _admin.Username,
                Email = _admin.Email,
                FullName = _admin.FullName,
            };

            var result = await _userManager.CreateAsync(admin , _admin.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(admin, "Admin");
            }
        }
    }
}
