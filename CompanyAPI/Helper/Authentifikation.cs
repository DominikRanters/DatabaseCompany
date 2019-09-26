using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CompanyAPI.Model;
using Microsoft.AspNetCore.Http;
using System.Text;
using Newtonsoft.Json;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;

namespace CompanyAPI.Helper
{
    public class Authentifikation
    {
        public static Payload GetUser(HttpContext context)
        {
            Payload retval = null;

            var authHeader = context.Request.Headers["Authorization"];

            if (!string.IsNullOrEmpty(authHeader))
            {
                var payload64Str = authHeader.ToString().Substring("Bearer ".Length).Trim().Split(".")[1];
                var payload = Encoding.ASCII.GetString(Convert.FromBase64String(payload64Str));

                retval = JsonConvert.DeserializeObject<Payload>(payload);
            }


            return retval;
        }

        public static bool IsUserAdmin(HttpContext context)
        {
            string url = "https://chaynssvc.tobit.com/v0.5/165143/user";
            string token = context.Request.Headers["Authorization"];

            WebRequest request = WebRequest.Create(url);
            if (request != null)
            {
                request.Method = "GET";
                request.ContentType = "application/json";
                request.Headers.Add("Authorization", token);

                using (Stream s = request.GetResponse().GetResponseStream())
                {
                    StreamReader sr = new StreamReader(s);
                    
                    var jsonResponse = sr.ReadToEnd();
                    JObject DataObj = JObject.Parse(jsonResponse);
                    var uacGroups = DataObj["data"]["uacGroups"];
                    var firstUACGroup = (int)uacGroups[0]["id"];
                    
                    return firstUACGroup == 1;

                }
            }
            return false;
        }

    }
}
