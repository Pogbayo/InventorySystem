
using InventorySystem.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace InventorySystem.Application.Interfaces.IRepositories
{
    public interface IAuthRepository
    {
        Task<IdentityResult?> RegisterUserAsync(ApplicationUser user, string password);
        Task<ApplicationUser?> AuthenticateUserAsync(string email, string password);
    }
}
