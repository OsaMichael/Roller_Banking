using Roller.DataContext.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text;

namespace Roller.Repository.Customers
{
   public class CustomerAccountDto 
    {
        public int Id { get; set; }
       // public int AccountId { get; set; }
        public int CustomerId { get; set; }
        public decimal Balance { get; set; }
        [Required]
        public string AccountNumber { get; set; }

       // public List<Account> accounts { get; set; }
        public virtual  CustomerDto CustomerDto { get; set; }
        public string PrintBalance()
        {
            return Balance.ToString("C", new CultureInfo("sv-SE"));
        }
        public CustomerAccountDto()
        {
            new CustomerDto();
   
            //  new UserProfile();
        }
    }
}
