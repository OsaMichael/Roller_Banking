using Microsoft.AspNetCore.Http;
using Roller.Repository.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Roller.Repository.Customers
{
   public class CustomerListViewModel
    {
        public int Total { get; set; }
        public int Id { get; set; }
        public int LoanId { get; set; }
        public int NumberOfPages { get; set; }
        public bool HasMorePages { get; set; }
        public bool HasPreviousPages { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public string City { get; set; }
        public string Balance { get; set; }
        public string AccountNumber { get; set; }
        public int CurrentPage { get; set; } = 1;
        [FileTypes("jpg,jpeg,png")]
        public IFormFile CustImageFile { get; set; }
        public string AccountUrl { get; set; }
        public string ImageUrl { get; set; }
        public string CustImageThumbnailUrl { get; set; }
        public string ScannThumbnailUrl { get; set; }

        public IEnumerable<CustomerListDto> Customers { get; set; }


    }
}
