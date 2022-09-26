using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace UserApi.Controllers
{
    [Route("/api/health")]
    [AllowAnonymous]
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
