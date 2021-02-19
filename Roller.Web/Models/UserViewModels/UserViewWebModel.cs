using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Roller.Web.Models.UserViewModels
{
    public class UserViewWebModel
    {
        public List<UserWebModel> Users { get; set; }

        public string Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        //public string MiddleName { get; set; }
        [Required]
        public string LastName { get; set; }
        //public string Department { get; set; }
        //public string Unit { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        //[Required]
        //public string Username { get; set; }

        public string Password { get; set; }
        public int Age { get; set; }
        [Required]
        public string Salary { get; set; }

    }
}
