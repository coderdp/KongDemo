using Libs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Mvc;

namespace OrderApi.Controllers
{
    [Route("/api/orders")]
    [Authorize]
    public class OrderController : ControllerBase
    {
        public OrderController(IHttpContextAccessor httpContextAccessor,
            IServer server, IWebHostEnvironment webHostEnvironment)
        {
            HttpContextAccessor = httpContextAccessor;
            Server = server;
            WebHostEnvironment = webHostEnvironment;
        }

        public IHttpContextAccessor HttpContextAccessor { get; }
        public IServer Server { get; }
        public IWebHostEnvironment WebHostEnvironment { get; }

        [HttpGet("")]
        public IActionResult Index()
        {
            var result = new ResponseResult()
            {
                AppName = WebHostEnvironment.ApplicationName,
                RawUrl = HttpContextAccessor.HttpContext.Request.Path.ToString(),
                Host = HttpContextAccessor.HttpContext.Request.Host.ToString()
            };

            var addresses = Server.Features.Get<IServerAddressesFeature>().Addresses;
            result.Addresses = string.Join(",", addresses);

            return Ok(result);
        }
    }
}
