using StroyToday.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace StroyToday.Common.Auth
{
    public class AuthenticationHelper
    {
        private readonly IJwtProvider _jwtProvider;

        public AuthenticationHelper(IJwtProvider jwtProvider)
        {
            _jwtProvider = jwtProvider;
        }

        public string GetUserId(HttpContext httpContext)
        {
            var token = httpContext.Request.Cookies["auth-token"];
            var userId = _jwtProvider.GetUserIdFromToken(token);

            return userId;
        }
    }
}
