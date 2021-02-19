using Microsoft.AspNetCore.Http;
using Roller.Repository.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Roller.Repository.Customers
{
   public class CustomerListDto
    {
        public int CustomerId { get; set; }
        public int LoanId { get; set; }
        public string GivenName { get; set; }
        public string Surname { get; set; }
        public string FullName { get; set; }
        public string City { get; set; }
        public string AccountNumber { get; set; }
        public string Country { get; set; }
        public decimal Balance { get; set; }
        public DateTime Birthdate { get; set; }
        [FileTypes("jpg,jpeg,png")]
        public IFormFile CustImageFile { get; set; }
        public string AccountUrl { get; set; }
        public string ImageUrl { get; set; }
        public string CustImageThumbnailUrl { get; set; }
        public string ScannThumbnailUrl { get; set; }
       

        public CustomerAddress Address { get; set; }
        public string NationalId { get; set; }

        public string GetFullName => GivenName + " " + Surname;

    }
}
