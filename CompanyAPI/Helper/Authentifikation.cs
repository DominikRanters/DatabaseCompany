using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CompanyAPI.Model;
using Microsoft.AspNetCore.Http;
using System.Text;
using Newtonsoft.Json;

namespace CompanyAPI.Helper
{
    public class Authentifikation
    {
        public static Payload GetUser(HttpContext context)
        {
            Payload retval = null;

            var authHeader = context.Request.Headers["Authorization"];

            if(!string.IsNullOrEmpty(authHeader))
            {
                var payload64Str = authHeader.ToString().Substring("Bearer ".Length).Trim().Split(".")[1];
                var payload = Encoding.ASCII.GetString(Convert.FromBase64String(payload64Str));

                retval = JsonConvert.DeserializeObject<Payload>(payload);
            }
            

            return retval;
        }
    }
}
