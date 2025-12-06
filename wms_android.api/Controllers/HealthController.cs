using Microsoft.AspNetCore.Mvc;

namespace wms_android.api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HealthController : ControllerBase
    {
        [HttpGet]
        [Route("/health")]
        public IActionResult Check()
        {
            return Ok(new { status = "healthy" });
        }
    }
} 