using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Roller.Repository.Accounts
{
   public class AccountDetailModel
    {
        public int AccountId { get; set; }
        [Required]
        public decimal? Balance { get; set; }
        public IList<DispositionDto> Dispositions { get; set; }
        public IList<LoanDto> Loans { get; set; }
        public IList<PermanentOrderDto> PermanentOrders { get; set; }

    }
    public class LoanDto
    {
        public int LoanId { get; set; }
        public string Status { get; set; }
        [Required]
        public decimal Amount { get; set; }
        public int Duration { get; set; }
        public DateTime Date { get; set; }
        public decimal Payments { get; set; }
    }

    public class PermanentOrderDto
    {
        public int Id { get; set; }
        public string AccountTo { get; set; }
        [Required]
        public decimal? Amount { get; set; }
        public string Bank { get; set; }
        public string Symbol { get; set; }
    }

    public class DispositionDto
    {
        public string Type { get; set; }
        public int CustomerId { get; set; }
    }
}
