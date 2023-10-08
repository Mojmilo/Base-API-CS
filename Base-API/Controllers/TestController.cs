using Base_API.Data;
using Base_API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Base_API.Controllers;

[ApiController]
[Route("[controller]")]
public class TestController : ControllerBase
{
    private readonly AppDbContext _context;
    
    public TestController([FromServices] AppDbContext context)
    {
        _context = context;
    }
    
    public async void Test()
    {
        // créer un client
        var customer = new Customer
        {
            Name = "John Doe",
            Email = "john.doe@gmail.com",
            Password = "1234",
            Address = "1 rue de la Paix",
            Phone = "0654897423"
        };
        
        // créer un fournisseur
        var supplier = new Supplier
        {
            Name = "John Doe",
            Email = "john.doe@gmail.com",
            Password = "1234",
            CompanyName = "John Doe Company",
        };
        
        // créer un item pour le fournisseur
        var item = new Item
        {
            Name = "Item 1",
            Supplier = supplier,
        };
        
        // créer une commande pour le client
        var order = new Order
        {
            Customer = customer,
        };
        
        // créer un itemOrder pour la commande
        var itemOrder = new ItemOrder
        {
            Item = item,
            Order = order,
            Quantity = 1,
        };
    }
}