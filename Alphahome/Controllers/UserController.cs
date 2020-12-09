using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Alphahome.Services.Interfaces;
using Alphahome.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;

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
        /// <param name="userInfo"></param>
        /// <returns></returns>
        [HttpPost("Authenticate")]
        public IActionResult Authenticate([FromBody] AlphahomeUser userInfo)
        {
            IActionResult response = Unauthorized();
            var ipAdress = ipAddress();
            var result = _alphahomeService.Authenticate(userInfo, ipAdress);
            if (result != null)
            {
                setTokenCookie(result.RefreshToken);
                return Ok(result) ;
            } else
            {
                return response;
            }

        }
        /// <summary>
        /// Lấy refresh_token và access_token
        /// </summary>
        /// <returns></returns>
        [HttpPost("refresh-token")]
        public IActionResult RefreshToken()
        {
            var refresh_token = Request.Cookies["refresh_token"];
            if (refresh_token == null)
            {
                return Unauthorized(new { message = "Invalid token" });
            }
            var response = _alphahomeService.RefreshToken(refresh_token, ipAddress());

            if (response == null)
                return Unauthorized(new { message = "Invalid token" });

            setTokenCookie(response.RefreshToken);

            return Ok(response);
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
                HttpOnly = false,
                Expires = DateTime.UtcNow.AddDays(7),
                IsEssential = true,
                Path = "/"
            };
            Response.Cookies.Append("refresh_token", token, cookieOptions);
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
