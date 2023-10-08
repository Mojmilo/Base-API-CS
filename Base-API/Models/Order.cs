namespace Base_API.Models;

public class Order
{
    public int Id { get; set; }
    
    public List<OrderItem> OrderItems { get; set; } = new();
    
    public Customer Customer { get; set; } = null!;
}