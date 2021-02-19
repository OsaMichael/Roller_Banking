using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Roller.Repository.AccountRepo
{
  public  class UserViewModel
    {
        public List<UserModel> Users { get; set; }

        public string Id { get; set; }
       // [Required]
        public string FirstName { get; set; }
        //public string MiddleName { get; set; }
       // [Required]
        public string LastName { get; set; }
        //public string Department { get; set; }
        //public string Unit { get; set; }
       // [Required]
        [EmailAddress]
        public string Email { get; set; }

        //[Required]
        //public string Username { get; set; }

        public string Password { get; set; }
        public int Age { get; set; }
      //  [Required]
        public string Salary { get; set; }
    }
}
