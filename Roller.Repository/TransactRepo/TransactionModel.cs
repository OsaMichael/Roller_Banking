using Roller.Repository.Customers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Roller.Repository.TransactRepo
{
   public class TransactionModel
    {
        public DateTime? Date { get; set; }
        public string Type { get; set; }
        public int customerId { get; set; }
        public string Operation { get; set; }
        public string Amount { get; set; }
        public string Balance { get; set; }
        public string Symbol { get; set; }

        public string Value { get; set; }

        public int Tr_Id { get; set; }
        public string Tr_Through { get; set; }
        public string Tr_EmpType { get; set; }
        public string Tr_AccName { get; set; }
        public string Tr_Type { get; set; }
        public double Tr_Amount { get; set; }
        public string Tr_Date { get; set; }
        public string Tr_Branch { get; set; }

        public virtual CustomerDto CustomerDtos { get; set; }


    }
}
