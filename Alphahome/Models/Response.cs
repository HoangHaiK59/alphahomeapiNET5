using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Alphahome.Models
{
    public class Response
    {
        public bool valid { get; set; }
        public string message { get; set; }
    }

    public class ResponseInsert
    {
        public bool valid { get; set; }
        public string message { get; set; }
    }

    public class AuthenticateResponse
    {
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Token { get; set; }

       //  [JsonIgnore] // refresh token is returned in http only cookie
        public string RefreshToken { get; set; }

        public AuthenticateResponse(UserModelGet user, string token, string refreshToken)
        {
            UserId = user.userId;
            FirstName = user.firstName;
            LastName = user.lastName;
            Token = token;
            RefreshToken = refreshToken;
        }
    }
}
