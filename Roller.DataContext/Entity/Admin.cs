using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Roller.DataContext.Entity
{
   public class Admin
    {
        [Key]
        public int Admin_Id { get; set; }
        public string Admin_Name { get; set; }
        public string Admin_address { get; set; }
        public string Admin_mobile { get; set; }

    }
}
