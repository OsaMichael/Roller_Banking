using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Roller.DataContext.Entity
{
   public class Branch
    {
        [Key]
        public int Branch_Id { get; set; }
        public string Branch_Location { get; set; }

    }
}
