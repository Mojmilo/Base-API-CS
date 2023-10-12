using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Base_API.Data;
using Base_API.Records.UserControllerRecords;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Base_API.Controllers;

[ApiController]
[Route("[controller]")]
public class IdentityController : ControllerBase
{
    private readonly AppDbContext _context;

    private const string TokenSecret = "x9RCWxv6Xb44vCqL8XUGxSketcosdVOaealY5tfRMQBz3fkRGSbO0VtgCocEI4ct";
    private static readonly TimeSpan TokenLifetime = TimeSpan.FromHours(1);
    
    public IdentityController([FromServices] AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("token")]
    public IActionResult GenerateToken([FromQuery] TokenGenerationRequest request)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(TokenSecret);
        
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(JwtRegisteredClaimNames.Sub, request.Email)
        };
        
        return Ok(new
        {
            token = tokenHandler.WriteToken(new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.Add(TokenLifetime),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            ))
        });
    }
}