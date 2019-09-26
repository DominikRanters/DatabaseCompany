using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chayns.Auth.ApiExtensions;
using Microsoft.AspNetCore.Http;
using CompanyAPI.Model;

namespace CompanyAPI.Helper
{
    public class ChaynsAuth
    {
        private readonly RequestDelegate _next;
        private ITokenInfoProvider _tokenInfoProvider;

        public ChaynsAuth(ITokenInfoProvider tokenInfoProvider, RequestDelegate next)
        {
            _tokenInfoProvider = tokenInfoProvider;
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var payload = _tokenInfoProvider.GetUserGroups();
            if (payload.Id == 1)
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
