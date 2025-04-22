

namespace InventorySystem.Application.DTOs.ApllicationUserDto
{
    public class UserGetDto
    {
        public required string Id { get; set; }
        public string? Email { get; set; }
        public IList<string> Roles { get; set; } = new List<string>();
    }
}
