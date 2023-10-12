using System.Security.Cryptography;
using Base_API.Data;
using Base_API.Models;
using Base_API.Records.UserControllerRecords;
using Base_API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Base_API.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly AppDbContext _context;
    
    public UsersController([FromServices] AppDbContext context)
    {
        _context = context;
    }
    
    [HttpGet(Name = "GetUsers")]
    public async Task<ActionResult<IEnumerable<User>>> Get()
    {
        return await _context.Users.ToListAsync();
    }
    
    [HttpGet("{id}", Name = "GetUser")]
    public async Task<ActionResult<User>> Get(int id)
    {
        return await _context.Users.FindAsync(id);
    }
    
    [HttpPost(Name = "CreateUser")]
    public async Task<ActionResult<User>> Create([FromBody] CreateUserRequest createUserRequest)
    {
        var user = new User
        {
            Name = createUserRequest.Name,
            Email = createUserRequest.Email,
            Password = UserService.HashPassword(createUserRequest.Password)
        };
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = user.Id }, user);
    }
    
    [HttpPut("{id}", Name = "UpdateUser")]
    public async Task<ActionResult<User>> Update(int id, [FromBody] UpdateUserRequest updateUserRequest)
    {
        var userInDb = await _context.Users.FindAsync(id);
        if (userInDb == null)
        {
            return NotFound();
        }

        userInDb.Name = updateUserRequest.Name;
        userInDb.Email = updateUserRequest.Email;
        _context.Entry(userInDb).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return userInDb;
    }
    
    [HttpDelete("{id}", Name = "DeleteUser")]
    public async Task<ActionResult<User>> Delete(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
        return user;
    }
    
    [HttpPut("{id}/change-password", Name = "ChangePassword")]
    public async Task<ActionResult<User>> ChangePassword(int id, [FromBody] ChangePasswordRequest changePasswordRequest)
    {
        var userInDb = await _context.Users.FindAsync(id);
        if (userInDb == null)
        {
            return NotFound();
        }
        
        if (changePasswordRequest.NewPassword != changePasswordRequest.ConfirmPassword)
        {
            return BadRequest(new { Message = "New password and confirm password do not match." });
        }
        
        if (userInDb.Password != UserService.HashPassword(changePasswordRequest.CurrentPassword))
        {
            return BadRequest(new { Message = "Current password is incorrect." });
        }
        
        if (userInDb.Password == UserService.HashPassword(changePasswordRequest.NewPassword))
        {
            return BadRequest(new { Message = "New password cannot be the same as the current password." });
        }

        userInDb.Password = UserService.HashPassword(changePasswordRequest.NewPassword);
        _context.Entry(userInDb).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return userInDb;
    }
}