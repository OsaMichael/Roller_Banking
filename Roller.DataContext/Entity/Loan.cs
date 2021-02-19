using System;
using System.Collections.Generic;
using System.Text;

namespace Roller.DataContext.Entity
{
  public  class Loan
    {
        public int LoanId { get; set; }
        public int AccountId { get; set; }
        public int CustId { get; set; }
        public int LOfficer_Id { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public int Duration { get; set; }
        public string  Deadline { get; set; }
        public decimal Payments { get; set; }
        public string Status { get; set; }
        public string AccountNumber { get; set; }
        public decimal Loan_Amount { get; set; }
        public decimal AmountTo_Pay { get; set; }
        public decimal Interest_Rate { get; set; }
        public decimal Loan_Amount_Paid { get; set; }
        public string Loan_Date { get; set; }
       // public int Loan_Deadline { get; set; }
        public string Manager_Approval { get; set; }
        public string LoanCause { get; set; }
        public string Frequency { get; set; }
        public decimal Total_Paid { get; set; }
        public bool IsActive { get; set; }

        
        public virtual Account Account { get; set; }
        public IEnumerable<SupperAccount> SupperAccount { get; set; }
    }
}
