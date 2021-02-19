using FluentValidation;
using Roller.DataContext;
using Roller.DataContext.Entity;
using Roller.Repository.Cards;
using Roller.Repository.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Roller.Repository.Customers
{
   public class CustomerDto
    {

        public ShowBalanceDto showBalance { get; set; }

        private string _gender;
        public int CustomerId { get; set; }
        //public int Id { get; set; }
        [Required]
        public string FullName => GivenName + " " + Surname;
        public string WritePhoneNumber => $"({TelephoneCountryCode}) {TelephoneNumber}";
        public string GivenName { get; set; }
        [Required]
        public string BVNNumber { get; set; }
        [Required]
        public string AccountNumber { get; set; }
        [Required]
        public string Surname { get; set; }
        public decimal Amount { get; set; }
        public decimal Balance { get; set; }
        public decimal ConfirmAmount { get; set; }
        public string ReferenceNo { get; set; }
        [Display(Name = "Name")]
        public string Depositor { get; set; }
        public string Attendant { get; set; }
        public string AccountType { get; set; }
        public string NationalId { get; set; }
        public DateTime? Birthday { get; set; }
        public string TelephoneNumber { get; set; }
        public string TelephoneCountryCode { get; set; }
        public string EmailAdress { get; set; }
        public string ImageUrl { get; set; }
        public string CustImageThumbnailUrl { get; set; }
        public string ScannThumbnailUrl { get; set; }
        public int TransactionPin { get; set; }
        public int ConfirmTransactionPin { get; set; }

        

        public string Gender
        {
            get => _gender.ToFirstLetterUpper();
            set => _gender = value;
        }

        public CustomerAddress Address { get; set; }

        public string PrintBirthday()
        {
            return Birthday.HasValue ? Birthday.Value.ToString("yyyy-MM-dd") : "";
        }

       // public string TotalBalance => Accounts.Sum(a => a.Balance).ToSwedishKrona();
        public virtual ICollection<CustomerAccountDto> Accounts { get; set; } //= new List<CustomerAccountDto>();
        public IEnumerable<Account> Accountss { get; set; }
        public IEnumerable<CardDto> Cards { get; set; } /*= new List<CardDto>();*/
        public IEnumerable<Transaction> Transactions { get; set; } /*= new List<CardDto>();*/

        public CustomerDto()
        {
            new HashSet<CustomerAccountDto>();
        }


        //public class CustomerAccountValidator : AbstractValidator<CustomerDto>
        //{
        //    public CustomerAccountValidator()
        //    {
        //        RuleFor(account => account.CreatedAt).NotNull().NotEmpty();
        //        RuleFor(account => account.Surname).NotNull().NotEmpty()
        //            .Matches("^[a-zA-z]+$");
        //        RuleFor(account => account.GivenName).NotNull()
        //            .NotEmpty().Matches("^[a-zA-Z]+$");
        //        RuleFor(account => account.GivenName).NotNull()
        //            .NotEmpty().Matches("^[a-zA-Z -]+$");
        //        RuleFor(account => account.TelephoneNumber).NotNull().NotEmpty()
        //            .Length(10).Matches("^[0-9]+$");
        //        RuleFor(account => account.EmailAdress).NotNull().NotEmpty().EmailAddress();
        //        //RuleFor(account => account.AccountType).NotNull().IsInEnum();
        //        RuleFor(account => account.TransactionPin).NotNull().Must(Validation.ValidLength);
        //        RuleFor(account => account.ConfirmTransactionPin).Equal(account => account.TransactionPin);
        //        RuleFor(account => account.BVNNumber).Length(10)
        //            .Matches("^[0-9]+$");

        //    }
    }
}
