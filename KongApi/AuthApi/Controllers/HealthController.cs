using Microsoft.AspNetCore.Mvc;

namespace AuthApi.Controllers
{
    [Route("/api/health")]
    public class HealthController : ControllerBase
    {
        public HealthController(IHttpContextAccessor httpContextAccessor)
        {
            HttpContextAccessor = httpContextAccessor;
        }

        public IHttpContextAccessor HttpContextAccessor { get; }

        [HttpGet("")]
        public IActionResult Index()
        {
            return Ok("ok");
        }
    }
}
