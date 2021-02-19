using Microsoft.AspNetCore.Identity;
using Roller.DataContext.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roller.Web.Models.RoleViewModel
{
    public class RoleEditModel
    {
        public Role Role { get; set; }
        public IEnumerable<User> Members { get; set; }
        public IEnumerable<User> NonMembers { get; set; }


    }
}
