using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Roller.DataContext.Entity
{
   public class TransferOfficer
    {
        [Key]
        public int TO_accId { get; set; }
        public string TO_name { get; set; }
        public string TO_address { get; set; }
        public string TO_mobile { get; set; }
        public string TO_Salary { get; set; }
        public string TO_LastPaymentDate { get; set; }
        public double TO_TotalPayment { get; set; }
        public string TO_Balance { get; set; }
        public string TO_branch { get; set; }

    }
}
