using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyAPI.Model
{
    public class Payload
    {
        public string Jti { get; set; }
        public string Sub { get; set; }
        public int Type { get; set; }
        public string Exp { get; set; }
        public string Iat { get; set; }
        public int LocationId { get; set; }
        public string SiteId { get; set; }
        public bool IsAdmin { get; set; }
        public int TobitUserId { get; set; }
        public string PersonId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
