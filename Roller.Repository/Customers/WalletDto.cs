using System;
using System.Collections.Generic;
using System.Text;

namespace Roller.Repository.Customers
{
   public class WalletDto
    {
        public decimal Amount { get; set; }
        public string AccountNumber { get; set; }
        public decimal AccountBalance { get; set; }
    }
}
