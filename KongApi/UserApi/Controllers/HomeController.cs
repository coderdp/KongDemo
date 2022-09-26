using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace UserApi.Controllers
{
    [Route("/api/home")]
    [Authorize]
    public class HomeController : ControllerBase
    {
        public HomeController()
        {

        }

        [HttpGet("")]
        public IActionResult WhoAmI()
        {
            var claims = User.Claims.Select(o => new
            {
                o.Type,
                o.Value
            }).ToList();

            return Ok(new { User.Identity.IsAuthenticated, claims });
        }

        [HttpGet("AllowAnonymous")]
        [AllowAnonymous]
        public IActionResult AllowAnonymous()
        {
            return Ok("AllowAnonymous Request");
        }
    }
}
