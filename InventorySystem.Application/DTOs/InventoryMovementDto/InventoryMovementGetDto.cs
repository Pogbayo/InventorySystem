using InventorySystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystem.Application.DTOs.InventoryMovementDto
{
    public class InventoryMovementGetDto
    {
        public Guid Id { get; set; }
        public string? ProductName { get; set; }
        public string? WarehouseName { get; set; }
        public int Quantity { get; set; }
        public MovementType MovementType { get; set; }
        public DateTime MovementDate { get; set; }
    }
}
