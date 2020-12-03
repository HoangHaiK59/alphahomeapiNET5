﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Alphahome.Services.Interfaces;
using Alphahome.Models;
using Microsoft.AspNetCore.Http;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace User.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IAlphahomeService _alphahomeService;
        public UserController(IAlphahomeService alphahomeService)
        {
            _alphahomeService = alphahomeService;
        }

        /// <summary>
        /// Đăng nhập
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost("Authenticate")]
        public IActionResult Authenticate([FromBody] AlphahomeUser user)
        {
            var result = _alphahomeService.Authenticate(user);
            if (result.valid)
            {
                return Ok(result);
            } else
            {
                return Ok(result);
            }

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
