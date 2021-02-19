using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Roller.DataContext.Entity
{
   public class Expense
    {
        [Key]
        public int Expense_Id { get; set; }
        public string Expense_name { get; set; }
        public string Expense_date { get; set; }
        public string Expense_amount { get; set; }

    }
}
