using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Roller.DataContext.Entity
{
   public class SupperAccount
    {
        [Key]
        public int SupperId { get; set; }
        public int? LoanId { get; set; }
        public string SupperAcctNumber { get; set; }
        public DateTime  TransDate { get; set; }
        public string TransactDate { get; set; }
        public decimal Amount { get; set; }
        public string CustAcctNumber { get; set; }
        public string SupperEmail { get; set; }
        public string SupperPhone { get; set; }


        public virtual Loan Loan { get; set; }
    }
}
