using InventorySystem.Application.Interfaces.IRepositories;
using InventorySystem.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace InventorySystem.Infrastructure.Respositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AuthRepository(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<ApplicationUser?> AuthenticateUserAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null || !await _userManager.CheckPasswordAsync(user,password))
                return null;
            return user;
        }

        public async Task<IdentityResult?> RegisterUserAsync(ApplicationUser user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }
    }
}
