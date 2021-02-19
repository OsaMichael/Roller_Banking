using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Roller.Repository.CashierRepo
{
  public  class TransferModel
    {
        [Required]
        public decimal Amount { get; set; }
        [Required]
        public string AccountNumber_from { get; set; }
        [Required]
        public string AccountNumber_to { get; set; }
        public string Description { get; set; }
        public string AccoutType { get; set; }
        [Required]
        public string FullName { get; set; }

    }
    public class TransferValidator : AbstractValidator<TransferModel>
    {
        public TransferValidator()
        {
           // RuleFor(intraBankTransfer => intraBankTransfer.CreatedAt).NotNull().NotEmpty();
            RuleFor(intraBankTransfer => intraBankTransfer.AccountNumber_from).NotNull().NotEmpty()
                .Length(10).Matches("^[0-9]+$"); ;
            RuleFor(intraBankTransfer => intraBankTransfer.AccountNumber_to).NotNull().NotEmpty()
             .Length(10).Matches("^[0-9]+$"); ;
            RuleFor(intraBankTransfer => intraBankTransfer.Amount).NotNull().NotEmpty().GreaterThan(0.0M);
            RuleFor(intraBankTransfer => intraBankTransfer.Description).NotNull().NotEmpty()
                .Matches("^[a-zA-Z ,.-]+$"); ;
            RuleFor(intraBankTransfer => intraBankTransfer.FullName).NotNull().NotEmpty()
                .Matches("^[a-zA-Z ]"); ;
            //RuleFor(intraBankTransfer => intraBankTransfer.RecipientAccountNumber).NotNull().NotEmpty()
            //    .Length(10).Matches("^[0-9]+$");
            //RuleFor(intraBankTransfer => intraBankTransfer.RecipientBankId).NotNull().NotEmpty().GreaterThan(0);
            ////RuleFor(intraBankTransfer => intraBankTransfer.RecipientBankName);
            //RuleFor(intraBankTransfer => intraBankTransfer.TransactionPin).NotNull().NotEmpty().Must(Validation.ValidLength);
        }
    }
}
