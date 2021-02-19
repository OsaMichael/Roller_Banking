using System;
using System.Collections.Generic;
using System.Text;

namespace Roller.Repository.Customers
{
   public class ShowBalanceDto
    {
        //public List<CustomerDto> customerDtos { get; set; }
        public string AccountNumber { get; set; }
        public decimal Balance { get; set; }
        public decimal Amount { get; set; }
        public string ReferenceNo { get; set; }
        public string Depositor { get; set; }
        public string Attendant { get; set; }
        public string DateAndTransId { get; set; }
        public string FullName { get; set; }
        public string Date { get; set; }
        public string RecipientEmail { get; set; }
        public string CustImageThumbnailUrl { get; set; }
        public bool IsActive { get; set; }

        public ShowBalanceDto()
        {
            IsActive = true;
        }
  
    }
}
