using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Roller.Repository.Accounts
{
  public  class AccountDebitModel
    {
        public int AccountId { get; set; }

        [DataType(DataType.Currency)]
        public decimal Amount { get; set; }

    }
}
