using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Roller.Repository.Customers
{
   public class CustomerModelValidator: AbstractValidator<CustomerProfile>
    {
        public CustomerModelValidator()
        {
            RuleFor(x => x.GivenName)
                    .MaximumLength(100).WithMessage("Maximum length is 100 characters")
                    .NotEmpty().WithMessage("Name is required")
                    .Matches(@"^[\p{L}]+$").WithMessage("Numbers or symbols now allowed");

            RuleFor(x => x.Surname)
                    .MaximumLength(100).WithMessage("Maximum length is 100 characters")
                    .NotEmpty().WithMessage("Name is required")
                    .Matches(@"^[\p{L}]+$").WithMessage("Numbers or symbols now allowed");

            //RuleFor(x => x.NationalId)
            //        .MaximumLength(20).WithMessage("Maximum length is 20 characters");

            RuleFor(x => x.TelephoneNumber)
                    .MaximumLength(25).WithMessage("Maximum length is 25 characters");

            //RuleFor(x => x.TelephoneCountryCode)
            //        .MaximumLength(10).WithMessage("Maximum length is 10 characters");

            RuleFor(x => x.EmailAdress)
                    .MaximumLength(100).WithMessage("Maximum length is 100 characters")
                    .EmailAddress().WithMessage("A valid email address is required");

            RuleFor(x => x.Gender)
                    .MaximumLength(6).WithMessage("Maximum length is 6 characters")
                    .NotEmpty().WithMessage("Select your gender");

            RuleFor(x => x.Address.StreetAdress)
                    .MaximumLength(100).WithMessage("Maximum length is 100 characters")
                    .NotEmpty().WithMessage("Street address is required");

            //RuleFor(x => x.Address.ZipCode)
            //        .MaximumLength(15).WithMessage("Maximum length is 15 characters")
            //        .NotEmpty().WithMessage("Zip code is required");

            RuleFor(x => x.Address.City)
                    .MaximumLength(100).WithMessage("Maximum length is 100 characters")
                    .NotEmpty().WithMessage("City is required")
                    .Matches(@"^[\p{L}]+$").WithMessage("Numbers or symbols now allowed");

            RuleFor(x => x.Address.Country)
                    .MaximumLength(100).WithMessage("Maximum length is 100 characters")
                    .NotEmpty().WithMessage("City is required")
                    .Matches(@"^[\p{L}]+$").WithMessage("Numbers or symbols now allowed");

            //RuleFor(x => x.Address.CountryCode)
            //        .MaximumLength(2).WithMessage("Maximum length is 2 characters")
            //        .NotEmpty().WithMessage("Country code is required")
            //        .Matches(@"^[\p{L}]+$").WithMessage("Numbers or symbols now allowed");

        }
    }
}
