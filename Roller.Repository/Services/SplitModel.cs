using System;
using System.Collections.Generic;
using System.Text;

namespace Roller.Repository.Services
{
   public class SplitModel
    {
        public string walletCode { get; set; }
        public decimal Amount { get; set; }
        public Boolean ShouldDeductFrom { get; set; }
    }
}
