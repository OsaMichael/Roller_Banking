using System;
using System.Collections.Generic;
using System.Text;

namespace Roller.Repository.UserClaim
{
   public class ChangeClaim
    {
        public string UserId { get; set; }
        public string NewClaim { get; set; }
        public string CurrentClaim { get; set; }
    }
}
