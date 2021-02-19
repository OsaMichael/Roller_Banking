using System;
using System.Collections.Generic;
using System.Text;

namespace Roller.Repository.Customers
{
  public  class GetCustomerQuery 
    {
        public List<CustomerProfile> CustomerProfiles { get; set; }
            public int CustomerId { get; set; }
        public string CustomerEmail { get; set; }
    }
}
