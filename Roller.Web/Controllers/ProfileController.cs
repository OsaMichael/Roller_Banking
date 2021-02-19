using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Roller.Repository.Customers;
using static Roller.Web.Controllers.Common.Enum;

namespace Roller.Web.Controllers
{
    public class ProfileController : BaseController
    {
        private ICustomerManager _custManager;
        private IHostingEnvironment _hostingEnvironment;

        public ProfileController(ICustomerManager custManager, IHostingEnvironment hostingEnvironment)
        {
            _custManager = custManager;
            _hostingEnvironment = hostingEnvironment;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Update()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var use = User.Identity.Name;

            var load = _custManager.GetCustomEmailwithUserSignin(use);

            EditProfileDto profile = new EditProfileDto();
            profile.EmailAdress = load.Emailaddress;
            profile.StreetAddress = load.Streetaddress;
            profile.Surname = load.Surname;
            profile.OtherNames = load.OtherNames;
            profile.GivenName = load.Givenname;
            profile.CustomerId = load.Id;

            return View(profile);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update([FromForm] EditProfileDto model)
        {
            if (ModelState.IsValid)
            {

                string LoginUser = User.Identity.Name;
                //var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                String Passport = "";
                //string Signature = "";
                var files = HttpContext.Request.Form.Files;

                if (files != null)
                {
                    // var file = Image;
                    //There is an error here
                    var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "uploads\\img\\");
                    //var upload = Path.Combine(_hostingEnvironment.WebRootPath, "upload\\img\\");
                    if (files.Count() > 0)
                    {
                        Passport = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(files[0].FileName);
                        //Signature = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(files[1].FileName);
                        //var fileName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(file.FileName);
                        var fileStream = new FileStream(Path.Combine(uploads, Passport), FileMode.Create);
                        //var fileStream2 = new FileStream(Path.Combine(upload, Signature), FileMode.Create);

                        files[0].CopyTo(fileStream);
                        //files[1].CopyTo(fileStream2);
                        // file.CopyTo(fileStream);
                        model.CustImageThumbnailUrl = Passport;
                        //model.ScannThumbnailUrl = Signature;

                    }
                    var result = await _custManager.UpdateProfile(model, LoginUser);

                    if (result)
                    {
                    
                        //alert message
                        TempData["Message"] = "updated " + " " + " " + " Successfully";

                        dynamic transRef = TempData["Message"];

                        Alert("success", transRef, NotificationType.success);

                        return RedirectToAction("IndexUsers", "Home");
                    }
                    else
                    {
                        TempData["Error"] = "bad";

                    }

                }
                //}
            }
            return View(model);
        }
    }
}