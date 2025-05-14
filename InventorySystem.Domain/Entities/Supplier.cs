using System.ComponentModel.DataAnnotations;

namespace InventorySystem.Domain.Entities;

public class Supplier
{
    public Guid SupplierId { get; set; }

    [Required, MaxLength(100)]
    public string Name { get; set; } = default!;

    [MaxLength(50)]
    public string ContactInfo { get; set; } = default!;

    [MaxLength(100)]
    public string ContactEmail { get; set; } = default!;
    public string Address { get; set; } = default!;

    public ICollection<Product>? Products { get; set; }


    public Supplier() { }

    public Supplier(string name, string contactInfo, string contactEmail, string address)
    {
        SupplierId = Guid.NewGuid();
        Name = name;
        ContactInfo = contactInfo;
        ContactEmail = contactEmail;
        Address = address;
    }
}
