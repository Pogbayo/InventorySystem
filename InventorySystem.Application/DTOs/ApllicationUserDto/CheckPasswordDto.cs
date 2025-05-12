using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystem.Application.DTOs.ApllicationUserDto
{
    public class CheckPasswordDto
    {
        public Guid UserId { get; set; }
        public required string Password { get; set; }
    }

}
