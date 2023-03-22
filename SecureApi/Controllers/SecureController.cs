using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SecureApi.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class SecureController : ControllerBase
{
    [HttpGet("Users")]
    public ActionResult Users()
    {
        return Ok();
    }

    [HttpGet("Admins")]
    [Authorize(Roles = "DudeAdmins")]
    public ActionResult Admins()
    {
        return Ok();
    }

    [HttpGet("Dudes")]
    [Authorize(Policy = "IsDude")]
    public ActionResult Dudes()
    {
        return Ok();
    }
}