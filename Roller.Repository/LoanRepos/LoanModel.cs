using Microsoft.AspNetCore.Http;
using Roller.Repository.Attributes;
using Roller.Repository.Customers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Roller.Repository.LoanRepos
{
  public  class LoanModel
    {
        public int LoanId { get; set; }
        public string SurnName { get; set; }
        [Required]
        [Display(Name = "First Name")]
        public string Givenname { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Phone { get; set; }
        public string Password { get; set; }
        public string AccountType { get; set; }
        [Required]
        public decimal Loan_Amount { get; set; }
        public decimal AmountTo_Pay { get; set; }
        public decimal Loan_Amount_Paid { get; set; }
        [Required]
        public decimal Interest_Rate { get; set; }
        [Display(Name = "Deadline (months)")]
        public int Duration { get; set; }
        public string Frequency { get; set; }
        public string Deadline { get; set; }
        public string LoanCause { get; set; }
        
        public string AccountNumber { get; set; }
        [FileTypes("jpg,jpeg,png")]
        public IFormFile CustImageFile { get; set; }
        public string AccountUrl { get; set; }
        public string ImageUrl { get; set; }
        public IFormFile ScannDocumentFile { get; set; }
      
        public string ScannThumbnailUrl { get; set; }
    
        public string CustImageThumbnailUrl { get; set; }
        [Required]
        [Display(Name = "Email adress")]
        [DataType(DataType.EmailAddress)]
        public string EmailAdress { get; set; }
        [Display(Name = "First name")]
        public string GivenName { get; set; }
        public string OtherNames { get; set; }
        [Required]
        public string BVNNumber { get; set; }
        public string LoanOfficier { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        [Required]
        [Display(Name = "Last name")]
        public string Surname { get; set; }
        public string Loan_Date { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        [Display(Name = "Birthday")]
        public DateTime? Birthday { get; set; }

        public List<CustomerListViewModel> customers { get; set; }


    }
}
