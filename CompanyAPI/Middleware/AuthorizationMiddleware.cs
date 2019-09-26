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
            if (context.Request.Method != "GET")
            {
                bool isAdmin = Authentifikation.IsUserAdmin(context);
                if (isAdmin)
                {
                    await _next(context);
                }
                else
                {
                    throw new Helper.RepoException(Helper.RepoResultType.FORBIDDEN);
                }
            }
        }
    }
}
