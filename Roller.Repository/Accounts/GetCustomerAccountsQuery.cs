using Roller.Repository.Customers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Roller.Repository.Accounts
{
    public class GetCustomerAccountsQuery
    {
        public IEnumerable<CustomerAccountDto> CustomerAccountDtos { get ;set;}
        public int CustomerId { get; set; }
    }
}
