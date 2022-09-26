using Libs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;

namespace OrderApi.Controllers
{
    [Route("/api/users")]
    [Authorize]
    public class UserController : ControllerBase
    {
        public UserController(IHttpContextAccessor httpContextAccessor,
            IServer server,
            IWebHostEnvironment webHostEnvironment)
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
            TransactionOptions options = new TransactionOptions();
            options.IsolationLevel = IsolationLevel.Serializable;
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
