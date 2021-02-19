using Roller.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roller.Web.Utility
{
    public class SenderModel
    {
      public string recepientName { get; set; }
       public string recipientEmail { get; set; }
        public string accountNumber { get; set; }
        public decimal Amount { get; set; }
        public decimal AmountGiven { get; set; }
        public decimal Interest_Rate { get; set; }
        public string Date { get; set; }
        public string Frequency { get; set; }
        public string Deadline { get; set; }
        public int  verificationCode { get; set; }

        public RecievedMail recievedMails { get; set; }

    }
}
