using System.ComponentModel.DataAnnotations;

namespace Base_API.Records.UserControllerRecords;

public record TokenGenerationRequest
{
    [Required]
    public string Email { get; set; } = string.Empty;
    [Required]
    public string Password { get; set; } = string.Empty;
}