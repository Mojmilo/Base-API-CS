using System.ComponentModel.DataAnnotations;

namespace Base_API.Records.UserControllerRecords;

public record CreateUserRequest
{
    [Required]
    public string Name { get; set; } = string.Empty;
    [Required]
    public string Email { get; set; } = string.Empty;
    [Required]
    public string Password { get; set; } = string.Empty;
}