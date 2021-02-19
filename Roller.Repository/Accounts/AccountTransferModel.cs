using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Roller.Repository.Accounts
{
  public  class AccountTransferModel
    {
        public int AccountIdTo { get; set; }

        public int AccountIdFrom { get; set; }

        [DataType(DataType.Currency)]
        public decimal Amount { get; set; }

    }
}
