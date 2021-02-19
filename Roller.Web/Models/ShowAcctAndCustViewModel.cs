using Roller.Repository.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roller.Web.Models
{
    public class ShowAcctAndCustViewModel
    {
      //public  List<CustomerDto>  customers { get; set; }
      //  public List<CustomerAccountDto> CustomerAccountDto { get; set; }

      public string AccountNumber { get; set; }
      public decimal Balance { get; set; }

    }
}
