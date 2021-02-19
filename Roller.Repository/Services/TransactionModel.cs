using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Roller.Repository.Services
{
  public  class TransactionModel
    {
        [Required]
        public string CustomerID { get; set; }
        public string Email { get; set; }
        [Required]
        public Decimal Amount { get; set; }

        public Decimal Value { get; set; }
        public string Item { get; set; }
        [Required]
        public string FullName { get; set; }
        public string Phone { get; set; }
        [Required]
        public string PaymentReference { get; set; }
        public string Split { get; set; }
        public string UniqueKey { get; set; }
        [Required]
        public Boolean IsReversal { get; set; }
        public string Reference { get; set; }
        public string feeTypePicked { get; set; }
        public string Channel { get; set; }
        public string Savecode { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime Date { get; set; }
        //public virtual FeeTypeModel FeeType { get; set; }
    }
}
