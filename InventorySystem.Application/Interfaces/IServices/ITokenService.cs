using InventorySystem.Domain.Entities;

namespace InventorySystem.Application.Interfaces.IServices
{
    public interface ITokenService
    {
        string GenerateAccessToken(ApplicationUser user);
        string GenerateRefreshToken();
    }

}
