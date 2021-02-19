using System;
using System.Collections.Generic;
using System.Text;

namespace Roller.Repository
{
  public  class RecievedMail
    {
        public string recepientName { get; set; }
        public string recipientEmail { get; set; }
        public string accountNumber { get; set; }
        public decimal Amount { get; set; }
        public string Date { get; set; }
        public int verificationCode { get; set; }
    }
}
