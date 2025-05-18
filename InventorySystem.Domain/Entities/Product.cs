using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventorySystem.Domain.Entities;
public class Product
{
    public Guid ProductId { get; set; }

    [Length(3,100)]
    public required string Name { get; set; } 

    //[Column(TypeName = "decimal(18,2)")]
    [Precision(18,2)]
    [Range(0,int.MaxValue)]
    public required decimal Price { get; set; }

    public required Guid CategoryId { get; set; }
    public required Guid SupplierId { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<ProductWarehouse> ProductWarehouses { get; set; } = [];
    public ICollection<InventoryMovement> InventoryMovement { get; set; } = [];

    [ForeignKey(nameof(CategoryId))]
    public Category Category { get; set; } = default!;
    [ForeignKey(nameof(SupplierId))]
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