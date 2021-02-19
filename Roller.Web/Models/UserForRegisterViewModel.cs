using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Roller.Web.Models
{
    public class UserForRegisterViewModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        [StringLength(14, MinimumLength = 4, ErrorMessage = "You must specify a password between 4 and 14 characters")]
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
    public class CustomerValidator : AbstractValidator<UserForRegisterViewModel>
    {
        public CustomerValidator()
        {
            // RuleFor(customer => customer.CreatedAt).NotNull().NotEmpty();
            RuleFor(customer => customer.Email).NotNull().NotEmpty()
                .Length(4, 20).Matches("^[a-zA-z 0-9_-]+$");
            RuleFor(customer => customer.Password).NotNull().NotEmpty()
                .MinimumLength(8);
            RuleFor(customer => customer.ConfirmPassword).Equal(customer => customer.Password);
            //  RuleFor(customer => customer.CustomerAccountNumber).NotNull().NotEmpty()
            //  .Length(10).Matches("^[0-9]+$");
            // RuleFor(customer => customer.VerificationCode).Must(ValidLength);
        }
    }
}
