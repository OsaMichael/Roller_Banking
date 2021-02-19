using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Roller.DataContext.Entity
{
   public class User: IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string State { get; set; }
        public int Age { get; set; }
     
        public DateTime DateOfBirth { get; set; }
        public string Salary { get; set; }
        public string Address { get; set; }
        public string LastPaymentDate { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal TotalPayment { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Balance { get; set; }
        public string branch { get; set; }
        public string ImageUrl { get; set; }
        public string ImageThumbnailUrl { get; set; }
        public string UploadDocument { get; set; }





        public virtual ICollection<IdentityUserRole<int>> Roles { get; set; } = new List<IdentityUserRole<int>>();

        public virtual ICollection<IdentityUserClaim<int>> Claims { get; set; } = new List<IdentityUserClaim<int>>();
       // [NotMapped]
        public string FullName
        {
            get
            {
                return this.LastName + " " + this.FirstName;
            }
        }
    }
}
