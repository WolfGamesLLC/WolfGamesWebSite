using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.Linq;
using System.Security.Claims;

namespace WolfGamesWebSite.Test.Framework.Fixtures
{
    public class AuthenticatedTestRequestMiddleware
    {
        public const string TestingCookieAuthentication = "TestCookieAuthentication";
        public const string TestingHeader = "X-Integration-Testing";
        public const string TestingHeaderValue = "abcde-12345";
        public const string TestingHeaderName = "my_name";
        public const string TestingHeaderId = "my_id";
        private readonly RequestDelegate _next;

        public AuthenticatedTestRequestMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Headers.Keys.Contains(TestingHeader) &&
                context.Request.Headers[TestingHeader].First().Equals(TestingHeaderValue))
            {
                if (context.Request.Headers.Keys.Contains(TestingHeaderName))
                {
                    var name =
                        context.Request.Headers[TestingHeaderName].First();
                    var id =
                        context.Request.Headers.Keys.Contains(TestingHeaderId)
                            ? context.Request.Headers[TestingHeaderId].First() : "";

                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, name),
                        new Claim(ClaimTypes.NameIdentifier, id),
                    }, TestingCookieAuthentication);
                    ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                    context.User = claimsPrincipal;
                }
            }
            await _next(context);
        }
    }
}
