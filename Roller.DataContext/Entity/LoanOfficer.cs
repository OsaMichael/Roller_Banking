using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Roller.DataContext.Entity
{
   public class LoanOfficer
    {
        [Key]
        public int LOfficer_Id { get; set; }
        public string LOfficer_name { get; set; }
        public string LOfficer_address { get; set; }
        public string LOfficer_mobile { get; set; }
        public double LOfficer_Salary { get; set; }
        public string LOfficer_LastPaymentDate { get; set; }
        public double LOfficer_TotalPayment { get; set; }
        public double LOfficer_Balance { get; set; }
        public string LOfficer_branch { get; set; }

       // public virtual IEnumerable<Loan> Loans { get; set; }
    }
}
