using AuthApi.ViewModels;
using Libs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthApi.Controllers
{
    [Route("/api/auth")]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private const string username = "admin";
        private const string password = "admin";

        public JwtOption JwtOption { get; }
        public AuthController(IOptions<JwtOption> option)
        {
            JwtOption = option.Value;
        }

        [HttpPost("token")]
        public IActionResult Token([FromBody] TokenReqeust tokenReqeust)
        {
            ArgumentNullException.ThrowIfNull(tokenReqeust);

            if (tokenReqeust.UserName != username || tokenReqeust.Password != password)
            {
                return BadRequest();
            }

            var token = GenerateJwtToken();

            return Ok(new { token });
        }

        private string GenerateJwtToken()
        {
            var claims = new List<Claim>();
            claims.Add(new Claim(type: "UserName", value: username));

            var keyBytes = Encoding.UTF8.GetBytes(JwtOption.Secret);

            var signingCredential = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256);

            var jwtToken = new JwtSecurityToken(
                issuer: JwtOption.Key,
                claims: claims,
                expires: DateTime.Now.AddMinutes(JwtOption.ExpireOfMinutes), signingCredentials: signingCredential);
            var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            return accessToken;
        }
    }
}
