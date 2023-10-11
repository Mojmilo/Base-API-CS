using System.ComponentModel.DataAnnotations.Schema;

namespace Base_API.Models;

[Table("Suppliers")]
public class Supplier : User
{
    public string CompanyName { get; set; } = string.Empty;
    
    public List<Item> Items { get; set; } = new();
}