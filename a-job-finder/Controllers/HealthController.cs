using Microsoft.AspNetCore.Mvc;
using a_job_finder.Interfaces;

namespace a_job_finder.Controllers;
[ApiController]
[Route("api/DbHealth")]
public class HealthController : BaseController
{
    private readonly IDbService _dbService;

    public HealthController(IDbService dbService)
    {
        _dbService = dbService;
    }

    [HttpGet("db-check")]
    public async Task<IActionResult> VerifyDatabase()
    {
        try
        {
            var isConnected = await _dbService.VerifyConnection();
            if (isConnected)
            {
                return AppResponse(200, "Database connection is healthy!");
            }
            return AppResponse(500, "Database connection failed! Verify your appsettings.json credentials.");
        }
        catch (Exception ex)
        {
            return AppResponse(500, $"Error verifying database: {ex.Message}");
        }
    }
}
