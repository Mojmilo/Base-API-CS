using Base_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Base_API.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; } = null!;
    
    public DbSet<Item> Items { get; set; } = null!;
    
    public DbSet<Order> Orders { get; set; } = null!;
    
    public DbSet<OrderItem> OrderItems { get; set; } = null!;

    public DbSet<Customer> Customers { get; set; } = null!;
    
    public DbSet<Supplier> Suppliers { get; set; } = null!;
}