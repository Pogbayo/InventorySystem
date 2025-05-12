using Microsoft.AspNetCore.Identity;

namespace InventorySystem.Domain.Entities;

public class ApplicationUser : IdentityUser<Guid>
{
    public string? Name { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public string? RefreshToken { get; set; }
    public DateTime RefreshTokenExpiryTime { get; set; }
}
