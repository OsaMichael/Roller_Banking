using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Roller.Web.Models
{
    public class RegisterViewModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        [Required]
        public string SelectedRole { get; set; }
        public string FirstName { get; set; }
        public string PhoneNumber { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        [Display(Name = "Birthday")]
        public DateTime? DateOfBirth { get; set; }
        public string Salary { get; set; }
        public string Address { get; set; }
        public string State { get; set; }
        public string LastPaymentDate { get; set; }
        public double TotalPayment { get; set; }
        public double Balance { get; set; }
        public string branch { get; set; }
        public string BVNNumber { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Gender { get; set; }
        public string Savings { get; set; }
        public string Current { get; set; }
        public string ImageUrl { get; set; }
        //[FileTypes("jpg,jpeg,png")]
        //public HttpPostedFileBase ImageFile { get; set; }
        public string ImageThumbnailUrl { get; set; }
        public string UploadDocument { get; set; }
        public CustomerAccountType AccountType { get; set; }

        public List<SelectListItem> RoleSelect { get; set; }

        public RegisterViewModel()
        {
            RoleSelect = new List<SelectListItem>
            {
                new SelectListItem("Select role", string.Empty, true, true),
                new SelectListItem(Claims.Admin, Claims.Admin),
                new SelectListItem(Claims.User, Claims.User),
                new SelectListItem(Claims.BranchManager, Claims.BranchManager),
                new SelectListItem(Claims.Cashier, Claims.Cashier),
                new SelectListItem(Claims.HROfficer, Claims.HROfficer),
                new SelectListItem(Claims.LoanOfficer, Claims.LoanOfficer),
                new SelectListItem(Claims.MDirector, Claims.MDirector),
                new SelectListItem(Claims.Officer, Claims.Officer),
            new SelectListItem(Claims.TransferOfficer, Claims.TransferOfficer),
            };

        }

    }
    public enum CustomerAccountType
    {
        Savings = 1,
        Current = 2,
    }
}
