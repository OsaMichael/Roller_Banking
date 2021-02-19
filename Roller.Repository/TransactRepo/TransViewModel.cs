using System;
using System.Collections.Generic;
using System.Text;

namespace Roller.Repository.TransactRepo
{
   public class TransViewModel
    {
        public int Total { get; set; }
        public int NumberOfPages { get; set; }
        public bool HasMorePages { get; set; }
        public int Tr_Id { get; set; }
        public string Tr_Through { get; set; }
        public string Tr_EmpType { get; set; }
        public string Tr_AccName { get; set; }
        public string Tr_Type { get; set; }
        public double Tr_Amount { get; set; }
        public string Tr_Date { get; set; }
        public string Tr_Branch { get; set; }
        public string AccountNumber { get; set; }



        private int _limit = 20;
        public int AccountId { get; set; }
        public int Offset { get; set; } = 0;

        public int Limit
        {
            get => _limit;
            set => _limit = value > 20 || value == 0 ? _limit : value;
        }

        public IEnumerable<TransactionModel> Transactions { get; set; }

    }
}
