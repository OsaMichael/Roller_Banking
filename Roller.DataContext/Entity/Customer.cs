using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Roller.DataContext.Entity
{
   public class Customer : BaseEntity.Entity
    {
        public Customer()
        {
            Dispositions = new HashSet<Disposition>();
            Accounts = new HashSet<Account>();
            Cards = new HashSet<Card>();
            Transactions = new HashSet<Transaction>();
        }

       // public int CustomerId { get; set; }
        public string Gender { get; set; }
        public string Givenname { get; set; }
        public string Surname { get; set; }
        public string FirstName { get; set; }
        public string OtherNames { get; set; }
        public string BVNNumber { get; set; }
        
        public string Streetaddress { get; set; }
        public string City { get; set; }
        public string Zipcode { get; set; }
        public string Country { get; set; }
        public string CountryCode { get; set; }
        public DateTime? Birthday { get; set; }
        public string NationalId { get; set; }
        public string Telephonecountrycode { get; set; }
        public string Telephonenumber { get; set; }
        public string Emailaddress { get; set; }
        public string Status { get; set; }
        public string CustAccNumber { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Cust_balance { get; set; }
        //public string Depositor { get; set; }
        //public string Attendant { get; set; }
        public string Cust_acc_type { get; set; }
        public string Deadline { get; set; }
        public string ImageUrl { get; set; }
        public string ImageThumbnailUrl { get; set; }
        public string UploadDocument { get; set; }
        public int TransactionPin { get; set; }
        //public CustomerAccountType AccountType { get; set; }

        public virtual ICollection<Disposition> Dispositions { get; set; }
        public virtual ICollection<Account> Accounts { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
        public virtual ICollection<Card> Cards { get; set; }

    }
    //public enum CustomerAccountType
    //{
    //    Savings = 1,
    //    Current = 2,
    //}
}
