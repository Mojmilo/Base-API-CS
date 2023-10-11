using System.ComponentModel.DataAnnotations.Schema;

namespace Base_API.Models;

[Table("Customers")]
public class Customer : User
{
    public string Address { get; set; } = string.Empty;
    
    public string Phone { get; set; } = string.Empty;
    
    public List<Order> Orders { get; set; } = new();
}