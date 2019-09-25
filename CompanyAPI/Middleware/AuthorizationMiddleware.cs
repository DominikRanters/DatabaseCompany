using CompanyAPI.Helper;
using CompanyAPI.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace CompanyAPI.Middleware
{
    public class AuthorizationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RepoExceptionMiddleware> _logger;

        public AuthorizationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            Payload user = Authentifikation.GetUser(context);

            if (user != null && user.PersonId == "131-62105")
            {
                await _next(context);
            }else
            {
                throw new Helper.RepoException(Helper.RepoResultType.FORBIDDEN);
            }
        }
    }
}
