using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Roller.DataContext.Entity
{
   public class OTPLog
    {
        [Key]
        public int OTPID { get; set; }
        public int OTP { get; set; }
        //public string Email { get; set; }
    }
}
