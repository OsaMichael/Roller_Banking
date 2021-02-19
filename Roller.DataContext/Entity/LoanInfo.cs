﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Roller.DataContext.Entity
{
  public  class LoanInfo
    {
        [Key]
        public int Loan_Id { get; set; }
        public int User_acc_no { get; set; }
        public int LOfficer_Id { get; set; }
        public int CustomerId { get; set; }
        public double Loan_Amount { get; set; }
        public double AmountTo_Pay { get; set; }
        public double Interest_Rate { get; set; }
        public double Loan_Amount_Paid { get; set; }
        public string Loan_Date { get; set; }
        public string Loan_Deadline { get; set; }
        public string Loan_Branch { get; set; }
        public string Manager_Approval { get; set; }
        public string MD_Approval { get; set; }
        public string Status { get; set; }
        public string LoanCause { get; set; }

        public LoanOfficer LoanOfficer { get; set; }

    }
}