using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text;

namespace Roller.Repository.Accounts
{
   public class CustomerAccountModel
    {
        public int AccountId { get; set; }
        public decimal Balance { get; set; }
        [Required]
        public int CustomerId { get; set; }

        public string PrintBalance()
        {
            return Balance.ToString("C", new CultureInfo("sv-SE"));
        }

    }
}
