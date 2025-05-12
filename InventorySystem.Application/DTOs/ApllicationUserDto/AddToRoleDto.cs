

namespace InventorySystem.Application.DTOs.ApllicationUserDto
{
    public class AddToRoleDto
    {
        public Guid UserId { get; set; }
        public string RoleName { get; set; } = string.Empty;
    }

}
