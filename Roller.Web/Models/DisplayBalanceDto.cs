using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roller.Web.Models
{
    public class DisplayBalanceDto
    {
        public decimal AccountBalance { get; set; }
        public string AccountNumber { get; set; }
    }
}
