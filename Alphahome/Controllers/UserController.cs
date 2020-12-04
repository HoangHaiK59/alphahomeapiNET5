using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Alphahome.Services.Interfaces;
using Alphahome.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace User.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IConfiguration _config;
        private readonly IAlphahomeService _alphahomeService;
        public UserController(IAlphahomeService alphahomeService, IConfiguration config)
        {
            _alphahomeService = alphahomeService;
            _config = config;
        }

        /// <summary>
        /// Đăng nhập
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost("Authenticate")]
        public IActionResult Authenticate([FromBody] AlphahomeUser userInfo)
        {
            IActionResult response = Unauthorized();
            var user = _alphahomeService.Authenticate(userInfo);
            if (user != null)
            {
                var token = GenerateJSONWebToken(userInfo);
                return Ok(new { token = token, userId = user.userId, firstName = user.firstName, lastName = user.lastName, email = user.email }) ;
            } else
            {
                return response;
            }

        }

        private string GenerateJSONWebToken(AlphahomeUser user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Email, user.email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(null,
              null,
              claims,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        //[HttpPost("revoke-token")]
        //public IActionResult RevokeToken([FromBody] RevokeTokenRequest model)
        //{
        //    // accept token from request body or cookie
        //    var token = model.Token ?? Request.Cookies["refreshToken"];

        //    if (string.IsNullOrEmpty(token))
        //        return BadRequest(new { message = "Token is required" });

        //    var response = _alphahomeService.RevokeToken(token, ipAddress());

        //    if (!response)
        //        return NotFound(new { message = "Token not found" });

        //    return Ok(new { message = "Token revoked" });
        //}

        private void setTokenCookie(string token)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(7)
            };
            Response.Cookies.Append("refreshToken", token, cookieOptions);
        }

        private string ipAddress()
        {
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
                return Request.Headers["X-Forwarded-For"];
            else
                return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }

    }
}
