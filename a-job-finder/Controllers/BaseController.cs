using Microsoft.AspNetCore.Mvc;

namespace a_job_finder.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BaseController : ControllerBase
{
    [NonAction]
    protected IActionResult AppResponse(int statusCode, string? message = null, object? data = null)
    {
        return StatusCode(statusCode, new
        {
            success = statusCode >= 200 && statusCode < 300,
            message,
            data
        });
    }
}
