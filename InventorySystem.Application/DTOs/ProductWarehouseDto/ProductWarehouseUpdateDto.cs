using System.ComponentModel.DataAnnotations;

namespace InventorySystem.Application.DTOs.ProductWarehouseDto
{
   public class ProductWarehouseUpdateDto
    {
        [Required]
        public int Quantity { get; set; }
    }

}
