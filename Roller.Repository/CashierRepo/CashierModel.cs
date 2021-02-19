using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Roller.Repository.CashierRepo
{
   public class CashierModel
    {
        public int Cashier_Id { get; set; }
        [Required]
        public string Cashier_Name { get; set; }
        public string Cashier_address { get; set; }
        [Required]
        public string Cashier_mobile { get; set; }
        public double Cashier_Salary { get; set; }
        public string Cashier_LastPaymentDate { get; set; }
        public double Cashier_TotalPayment { get; set; }
        public double Cashier_Balance { get; set; }
        public string Cashier_branch { get; set; }

    }
}
