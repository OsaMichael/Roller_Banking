using System;
using System.Collections.Generic;
using System.Text;

namespace Roller.Repository.Services
{
   public class Data
    {
        public string transactionReference { get; set; }
        public long charge { get; set; }
        public string redirectUrl { get; set; }
        public string Status { get; set; }
        public string message { get; set; }
        public string reference { get; set; }
    }
}
