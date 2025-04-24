

using InventorySystem.Application.DTOs.ApllicationUserDto;

namespace InventorySystem.Application.Interfaces.IServices
{
    public interface IAuthService
    {
        Task<UserGetDto?> RegisterUserAsync(UserCreateDto userdetails);
        Task<string?> LoginAsync(string email, string password);
    }
}
