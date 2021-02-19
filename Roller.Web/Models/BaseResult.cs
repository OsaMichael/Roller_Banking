using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roller.Web.Models
{
    public class BaseResult
    {
        public bool IsSuccess { get; set; }
        public string Success { get; set; }
        public string Error { get; set; }
    }
}
