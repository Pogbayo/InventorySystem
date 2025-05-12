using InventorySystem.Domain.Entities;

namespace InventorySystem.Application.Interfaces.IServices
{
    public interface ITokenService
    {
        string GenerateAccessToken(ApplicationUser user,IList<string> roles);
        string GenerateRefreshToken();
    }

}
