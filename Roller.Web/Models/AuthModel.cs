using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roller.Web.Models
{
    public class AuthModel
    {
        public string Id { get; set; }
        public string Token { get; set; }
        public DateTime Exp { get; set; }
        public string Role { get; set; }
    }
}
