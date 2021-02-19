using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Roller.DataContext.Entity
{
   public class Transaction
    {
        [Key]
        public int TransactionId { get; set; }
       // public int AccountId { get; set; }
        public int? CustomerId { get; set; }
        public DateTime? Date { get; set; }
        public string Type { get; set; }
        public string Operation { get; set; }
        public decimal Amount { get; set; }
        public decimal Balance { get; set; }
        public string Symbol { get; set; }
        public string Bank { get; set; }
        public string Account { get; set; }
        public string AccountNumber { get; set; }
        public string Tr_Through { get; set; }
        public string Tr_EmpType { get; set; }
        public string Tr_AccName { get; set; }
        public string Tr_Date { get; set; }
        public string Tr_Branch { get; set; }
        public string ReferenceNo { get; set; }
        public string Depositor { get; set; }
        public string Attendant { get; set; }



      //  public virtual Account AccountNavigation { get; set; }
        public virtual Customer Customers { get; set; }
    }
}
