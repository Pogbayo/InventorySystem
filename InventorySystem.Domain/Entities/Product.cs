using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventorySystem.Domain.Entities;
public class Product
{
    public Guid ProductId { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = default!;

    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }

    [Required]
    public Guid CategoryId { get; set; }
    [Required]
    public Guid SupplierId { get; set; }

    public DateTime CreatedAt { get; set; }

    public ICollection<ProductWarehouse>? ProductWarehouses { get; set; }
    public ICollection<InventoryMovement>? InventoryMovement { get; set; }

    [ForeignKey("CategoryId")]
    public Category Category { get; set; } = default!;
    [ForeignKey("SupplierId")]
    public Supplier Supplier { get; set; } = default!;


    public Product() { }

    public Product(string name, decimal price, Guid categoryId,Guid supplierId)
    {
        Name = name;
        Price = price;
        CategoryId = categoryId;
        SupplierId = supplierId;
    }
}