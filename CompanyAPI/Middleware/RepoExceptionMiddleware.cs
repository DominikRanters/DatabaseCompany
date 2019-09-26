using CompanyAPI.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using CompanyAPI.Interface;
using CompanyAPI.Model;
using CompanyAPI.Model.Dto;
using Microsoft.AspNetCore.Mvc;

namespace CompanyAPI.Middleware
{
    public class RepoExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RepoExceptionMiddleware> _logger;

        public RepoExceptionMiddleware(RequestDelegate next, ILoggerFactory logger)
        {
            _next = next;
            _logger = logger.CreateLogger<RepoExceptionMiddleware>();
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (RepoException repoEx)
            {
                switch (repoEx.ExType)
                {
                    case RepoResultType.SQL_ERROR:
                        _logger.LogError(repoEx.InnerException, "ServiceUnavailable " + repoEx.Message);
                        context.Response.StatusCode = (int)HttpStatusCode.ServiceUnavailable;
                        break;
                    case RepoResultType.NOTFOUND:
                        _logger.LogError(repoEx.InnerException, "Conflict " + repoEx.Message);
                        context.Response.StatusCode = (int)HttpStatusCode.Conflict;
                        break;
                    case RepoResultType.WRONGPARAMETER:
                        _logger.LogError(repoEx.InnerException, "BadRequest " + repoEx.Message);
                        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    case RepoResultType.FORBIDDEN:
                        _logger.LogError(repoEx.InnerException, "Forbidden " + repoEx.Message);
                        context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                        break;
                    default:
                        _logger.LogCritical(repoEx.InnerException, "default Conflict " + repoEx.Message);
                        context.Response.StatusCode = (int)HttpStatusCode.Conflict;
                        break;
                }
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "Request failed ver heavily");
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }
        }

    }
}
