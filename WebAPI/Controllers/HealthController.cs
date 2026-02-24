using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            Console.WriteLine("Health check endpoint was called.");
            return Ok(new { status = "Healthy", timestamp = DateTime.UtcNow });
        }
    }
}
