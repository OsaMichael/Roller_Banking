using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Roller.Repository.Accounts
{
   public class AccountDebitCommandValidator: AbstractValidator<AccountDebitModel>
    {
        public AccountDebitCommandValidator()
        {
            RuleFor(x => x.Amount)
                .ScalePrecision(2, 13).WithMessage("Maximum 13 digits and 2 decimals")
                .InclusiveBetween(1.00m, 99999999999.99m).WithMessage("Can't enter a negative amount.");
        }
    }
}
