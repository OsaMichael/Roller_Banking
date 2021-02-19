using FluentValidation;
using Microsoft.AspNetCore.Http;
using Roller.DataContext;
using Roller.Repository.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Roller.Repository.Customers
{
    public class CustomerProfile /*: BaseViewModel*/
    {
        public int CustomerId { get; set; }

        [Display(Name = "First name")]
        public string GivenName { get; set; }
        public string OtherNames { get; set; }
        //public int? AccountType { get; set; }
        public string AccountType { get; set; }
      //  public long CreatedAt { get; set; }

        [Display(Name = "Last name")]
        public string Password { get; set; }
        public string Surname { get; set; }

        [Display(Name = "Birthday")]
        public DateTime? Birthday { get; set; }
       // public string DateOfBirth { get; set; }

        [Display(Name = "Phone number")]
        [DataType(DataType.PhoneNumber)]
        public string TelephoneNumber { get; set; }

        //[Display(Name = "Phone country code")]
        //public string TelephoneCountryCode { get; set; }
        public string BVNNumber { get; set; }

        [Display(Name = "Email adress")]
        [DataType(DataType.EmailAddress)]
        public string EmailAdress { get; set; }

        [Display(Name = "Gender")]
        public string Gender { get; set; }
        [FileTypes("jpg,jpeg,png")]
        public IFormFile CustImageFile { get; set; }
        public string AccountUrl { get; set; }
        public string ImageUrl { get; set; }
        public string StreetAddress { get; set; }
        public IFormFile ScannDocumentFile { get; set; }
        public string ScannThumbnailUrl { get; set; }
        public string CustImageThumbnailUrl { get; set; }
        public CustomerAddress Address { get; set; }
        public bool IsActive { get; set; }

        //public CustomerProfile()
        //{
        //    IsActive = true;
        //}
        //  public int TransactionPin { get; set; }
        // public int ConfirmTransactionPin { get; set; }

    }
    //public class CustomerAccountValidator : AbstractValidator<CustomerProfile>
    //{
    //    public CustomerAccountValidator()
    //    {
    //       // RuleFor(account => account.CreatedAt).NotNull().NotEmpty();
    //        RuleFor(account => account.Surname).NotNull().NotEmpty()
    //            .Matches("^[a-zA-z]+$");
    //        RuleFor(account => account.GivenName).NotNull()
    //            .NotEmpty().Matches("^[a-zA-Z]+$");

    //        RuleFor(account => account.OtherNames).NotNull()
    //            .NotEmpty().Matches("^[a-zA-Z -]+$");

    //        //RuleFor(account => account.Birthday).NotNull().NotEmpty()
    //        //.Matches("^[a-zA-z]+$");

    //        RuleFor(account => account.GivenName).NotNull()
    //            .NotEmpty().Matches("^[a-zA-Z]+$");
    //        RuleFor(account => account.Gender).NotNull()
    //         .NotEmpty().Matches("^[a-zA-Z]+$");
    //        RuleFor(account => account.AccountType).NotNull()
    //       .NotEmpty().Matches("^[a-zA-Z]+$");

    //        RuleFor(account => account.TelephoneNumber).NotNull().NotEmpty()
    //            .Length(10).Matches("^[0-9]+$");
    //        RuleFor(account => account.EmailAdress).NotNull().NotEmpty().EmailAddress();
    //        //RuleFor(account => account.AccountType).NotNull().IsInEnum();
    //       // RuleFor(account => account.TransactionPin).NotNull().Must(Validation.ValidLength);
    //       // RuleFor(account => account.ConfirmTransactionPin).Equal(account => account.TransactionPin);
    //        RuleFor(account => account.BVNNumber).Length(10)
    //            .Matches("^[0-9]+$");

    //      //  RuleFor(x => x.Address.Country)
    //      //     .MaximumLength(100).WithMessage("Maximum length is 100 characters")
    //      //     .NotEmpty().WithMessage("City is required")
    //      //     .Matches(@"^[\p{L}]+$").WithMessage("Numbers or symbols now allowed");

    //      //  RuleFor(x => x.Address.City)
    //      //.MaximumLength(100).WithMessage("Maximum length is 100 characters")
    //      //.NotEmpty().WithMessage("City is required")
    //      //.Matches(@"^[\p{L}]+$").WithMessage("Numbers or symbols now allowed");


    //    }
    //}
}
