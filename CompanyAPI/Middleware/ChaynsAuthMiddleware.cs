using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CompanyAPI.Helper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace CompanyAPI.Middleware
{
    public static class ChaynsAuthMiddleware
    {
        public static void UseChaynsAuthMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ChaynsAuth>();
        }
    }
}
