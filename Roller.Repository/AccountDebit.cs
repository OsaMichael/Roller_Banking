using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roller.Web.Utility
{
    public class AccountDebit
    {
        public decimal Loan_Amount_Paid { get; set; }
        public decimal Loan_Amount { get; set; }
        public decimal Interest_Rate { get; set; }
        public string Loan_Date { get; set; }
        public decimal AmountTo_Pay { get; set; }
        public int Duration { get; set; }
        public string Deadline { get; set; }
        public string AccountNumber { get; set; }
    }
}
