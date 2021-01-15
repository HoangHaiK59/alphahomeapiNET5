using Alphahome.Models;
using Alphahome.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alphahome.State
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;
        public JwtMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }
        public async Task Invoke(HttpContext context, IAlphahomeService service)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").First() != "Basic")
            {
                if (token != null)
                    await attachUser(context, token, service);
            } else
            {
            }
            // context.Request.Headers.Clear();
            await _next(context);
        }
        private async Task attachUser(HttpContext context, string token, IAlphahomeService service)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero

                }, out SecurityToken validatedToken);
                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = (jwtToken.Claims.First(x => x.Type == "sub").Value);
                context.Items["Account"] = userId;
                if (!string.IsNullOrEmpty(userId))
                {

                    // Guid.TryParse(userId, out var guidId);
                    var user = await service.FindUserAsync(userId);
                    if (user.valid)
                    {
                        await _next.Invoke(context);
                    } else
                    {
                        await _next(null);
                    }
                }
                else
                {
                    await _next(null);
                }
            } 
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }

}
