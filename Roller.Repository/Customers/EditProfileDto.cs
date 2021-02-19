using Microsoft.AspNetCore.Http;
using Roller.Repository.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Roller.Repository.Customers
{
  public  class EditProfileDto
    {
        public int CustomerId { get; set; }
        [Display(Name = "First name")]
        public string GivenName { get; set; }
        public string OtherNames { get; set; }
        public string Surname { get; set; }
        [Display(Name = "Email adress")]
        [DataType(DataType.EmailAddress)]
        public string EmailAdress { get; set; }
        public string StreetAddress { get; set; }
        [FileTypes("jpg,jpeg,png")]
        public IFormFile CustImageFile { get; set; }
        public string CustImageThumbnailUrl { get; set; }
    }
}
