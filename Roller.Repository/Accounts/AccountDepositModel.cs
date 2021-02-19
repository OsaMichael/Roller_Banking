using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Roller.Repository.Accounts
{
   public class AccountDepositModel
    {
        public int AccountId { get; set; }

        [DataType(DataType.Currency)]
        [Required]
        public decimal Amount { get; set; }

    }
}
