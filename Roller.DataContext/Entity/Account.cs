using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Roller.DataContext.Entity
{
   public class Account/*BaseEntity.Entity*/
    {
        public Account()
        {
            //Dispositions = new HashSet<Disposition>();
         //   Transactions = new HashSet<Transaction>();
            Loans = new HashSet<Loan>();
            PermenentOrders = new HashSet<PermenentOrder>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AccountId { get; set; }
        public int CustId { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public string Frequency { get; set; }
        // public int TransactionId { get; set; }
        public DateTime Created { get; set; }
        public decimal? Balance { get; set; }
        public string AccountNumber { get; set; }
        public virtual Customer customer { get; set; }

        //public virtual ICollection<Disposition> Dispositions { get; set; }
      //  public virtual ICollection<Transaction> Transactions { get; set; }
        public virtual ICollection<PermenentOrder> PermenentOrders { get; set; }
        public virtual ICollection<Loan> Loans { get; set; }
    }
}
