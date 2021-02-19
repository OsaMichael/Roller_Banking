using System;
using System.Collections.Generic;
using System.Text;

namespace Roller.Repository.Customers
{
   public class GetCustomerDetailsQuery: CustomerDto
    {
       // public List<CustomerDto> CustomerDtos { get; set; }
        public int CustomerId { get; set; }
        public int Id { get; set; }
        public string AccountNumber { get; set; }
        public string CustomerEmail { get; set; }
    }
}
