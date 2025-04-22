using System.ComponentModel.DataAnnotations;

namespace InventorySystem.Domain.Entities;

public class Category
{
    public Guid CategoryId { get; set; }
    public string Name { get; set; } = default!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public ICollection<Product> Products { get; set; } = new List<Product>();

    public Category() { }

    public Category (string name)
    {
        Name = name; 
    }
}
