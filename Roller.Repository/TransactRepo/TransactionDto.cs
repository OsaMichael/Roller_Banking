using System;
using System.Collections.Generic;
using System.Text;

namespace Roller.Repository.TransactRepo
{
   public class TransactionDto
    {
        public string Attendant { get; set; }
        public string Depositor { get; set; }
        public string Type { get; set; }
        public string ReferenceNo { get; set; }
        public decimal Amount { get; set; }
        public string Tr_Date { get; set; }
      //  public string Tr_Branch { get; set; }
        public string AccountNumber { get; set; }
    }
}
