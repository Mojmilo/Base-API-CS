namespace Base_API.Models;

public class Item
{
    public int Id { get; set; }
    
    public string Name { get; set; } = string.Empty;
    
    public List<ItemOrder> ItemOrders { get; set; } = new();
    
    public Supplier Supplier { get; set; } = null!;
}