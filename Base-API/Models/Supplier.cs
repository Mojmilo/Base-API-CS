namespace Base_API.Models;

public class Supplier : User
{
    public string CompanyName { get; set; } = string.Empty;
    
    public List<Item> Items { get; set; } = new();
}