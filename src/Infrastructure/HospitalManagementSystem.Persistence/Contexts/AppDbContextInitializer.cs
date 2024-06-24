using HospitalManagementSystem.Domain.Entities.Identity;
using HospitalManagementSystem.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace HospitalManagementSystem.Persistence.Contexts
{
    public class AppDbContextInitializer
    {
        private readonly AppDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _configuration;

        public AppDbContextInitializer(
            AppDbContext context,
            RoleManager<IdentityRole> roleManager, 
            UserManager<AppUser> userManager,
            IConfiguration configuration
            )
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
            _configuration = configuration;
        }
        public async Task InitializeDbContext()
        {
            await _context.Database.MigrateAsync();
        }
        public async Task CreateRolesAsync()
        {
            foreach(Role role in Enum.GetValues(typeof(Role)))
            {
                if (!await _roleManager.RoleExistsAsync(role.ToString()))
                    await _roleManager.CreateAsync(new IdentityRole { Name=role.ToString()});
            }
        }
        public async Task InitializeAdmin()
        {
            AppUser admin = new AppUser { 
                Name = "admin",
                Surname = "admin",
                Email = _configuration["AdminSettings:Email"],
                UserName = _configuration["AdminSettings:Username"]
            };
            await _userManager.CreateAsync(admin, _configuration["AdminSettings:Password"]);
            await _userManager.AddToRoleAsync(admin, Role.Admin.ToString());
        }
    }
}
