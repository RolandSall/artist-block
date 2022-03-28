using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace account_service.Controllers.Auth0TestController;

[Route("api")]
[ApiController]
public class Auth0TestController : ControllerBase
{
    [HttpGet("public")]
    public IActionResult Public()
    {
        return Ok("Hello from a public endpoint! You don't need to be authenticated to see this!!!");
    }

    [HttpGet("private")]
    [Authorize]
    public IActionResult Private() // getUserId
    {
        // var auth0Id = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var claims = User.Claims;
        
        return Ok(new
        {
            Message = "Hello from a private endpoint! You need to be authenticated to see this!",
            claims = claims,
        });
    }

    [HttpGet("private-scoped")]
    [Authorize("read:messages")]
    public IActionResult Scoped()
    {
                
        return Ok(new
        {
            Message = "Hello from a private endpoint! You need to be authenticated and have a scope of read:messages to see this!"
        });
    }

    // This is a helper action. It allows you to easily view all the claims of the token.
    [HttpGet("claims")]
    public IActionResult Claims()
    {
        return Ok(User.Claims.Select(c =>
            new
            {
                c.Type,
                c.Value
            }));
    }
}
    
