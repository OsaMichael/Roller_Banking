using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Roller.DataContext.Entity
{
    public class Disposition
    {
        public Disposition()
        {
            Cards = new HashSet<Card>();
        }

        public int DispositionId { get; set; }
        public int CustomerId { get; set; }
        public int AccountId { get; set; }
        public string Type { get; set; }
        public DateTime? Date { get; set; }

        public virtual Account Account { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual ICollection<Card> Cards { get; set; }
    }
}
