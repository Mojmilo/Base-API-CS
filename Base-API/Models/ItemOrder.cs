using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Base_API.Models;

[PrimaryKey(nameof(ItemId), nameof(OrderId))]
public class ItemOrder
{
    public int ItemId { get; set; }
    
    public int OrderId { get; set; }
    
    public Item Item { get; set; } = null!;
    
    public Order Order { get; set; } = null!;
    
    [Required]
    public int Quantity { get; set; }
}