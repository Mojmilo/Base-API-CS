using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Base_API.Controllers;

[ApiController]
[Route("[controller]")]
public class VersionController : ControllerBase
{
    [HttpGet(Name = "GetVersion")]
    public Settings Get([FromServices] IOptions<Settings> settings)
    {
        return settings.Value;
    }
}