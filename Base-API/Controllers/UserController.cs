using Base_API.Data;
using Base_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Base_API.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    [HttpGet(Name = "GetUsers")]
    public async Task<ActionResult<IEnumerable<User>>> Get([FromServices] AppDbContext context)
    {
        return await context.Users.ToListAsync();
    }
    
    [HttpGet("{id}", Name = "GetUser")]
    public async Task<ActionResult<User>> Get([FromServices] AppDbContext context, int id)
    {
        return await context.Users.FindAsync(id);
    }
    
    [HttpPost(Name = "CreateUser")]
    public async Task<ActionResult<User>> Create([FromServices] AppDbContext context, [FromBody] User user)
    {
        await context.Users.AddAsync(user);
        await context.SaveChangesAsync();
        return user;
    }
    
    [HttpPut("{id}", Name = "UpdateUser")]
    public async Task<ActionResult<User>> Update([FromServices] AppDbContext context, int id, [FromBody] User user)
    {
        if (id != user.Id)
        {
            return BadRequest();
        }

        context.Entry(user).State = EntityState.Modified;
        await context.SaveChangesAsync();
        return user;
    }
    
    [HttpDelete("{id}", Name = "DeleteUser")]
    public async Task<ActionResult<User>> Delete([FromServices] AppDbContext context, int id)
    {
        var user = await context.Users.FindAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        context.Users.Remove(user);
        await context.SaveChangesAsync();
        return user;
    }
}