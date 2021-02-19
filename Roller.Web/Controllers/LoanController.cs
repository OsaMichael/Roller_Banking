using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

using Roller.Repository;
using Roller.Repository.Customers;
using Roller.Repository.LoanRepos;
using Roller.Web.Utility;
using Microsoft.Extensions.Configuration;
using AutoMapper;
using Roller.Web.Models;
using Microsoft.AspNetCore.Authorization;
using static Roller.Web.Controllers.Common.Enum;
using Roller.DataContext.Entity;

namespace Roller.Web.Controllers
{
    //[Authorize(Policy = Claims.Cashier)]
    public class LoanController : BaseController
    {
        private ILoanRepository _loanRepo;
        private ICustomerManager _custManager;
        private IHostingEnvironment _hostingEnvironment;
        private INotification _notification;
        private IEmailService _emailSending;
        private ElasticEmailService _ElasticEmailService;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        private IEmailSender _emailSender;
        public LoanController(ILoanRepository loanRepo, ICustomerManager custManager,
            IHostingEnvironment hostingEnvironment, INotification notification, 
            IConfiguration config,  IMapper mapper, IEmailService emailSending, IEmailSender emailSender)
        {
            _loanRepo = loanRepo;
            _custManager = custManager;
            _hostingEnvironment = hostingEnvironment;
            _notification = notification;
            _config = config;
            _mapper = mapper;
            _emailSending = emailSending;
            _emailSender = emailSender;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult RegisterLoan()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
       //[Authorize(Policy = Claims.Cashier)]
        public async Task<IActionResult> RegisterLoan([FromForm] LoanModel model)
        {

            if (ModelState.IsValid)
            {

                string AccountNumber = Generate.GenerateAccountNumber();
            while (_custManager.GetAccountViaAccountNumber(AccountNumber) != null)
            {
                AccountNumber = Generate.GenerateAccountNumber();
            }

            string LoginUser = User.Identity.Name;
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);


            String Passport = "";
            string Signature = "";
            var files = HttpContext.Request.Form.Files;

            if (files != null)
            {
                // var file = Image;
                //There is an error here
                var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "uploads\\img\\");
                var upload = Path.Combine(_hostingEnvironment.WebRootPath, "upload\\img\\");
                if (files.Count() > 0)
                {
                    Passport = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(files[0].FileName);
                    Signature = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(files[1].FileName);
                    //var fileName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(file.FileName);
                    var fileStream = new FileStream(Path.Combine(uploads, Passport), FileMode.Create);
                    var fileStream2 = new FileStream(Path.Combine(upload, Signature), FileMode.Create);

                    files[0].CopyTo(fileStream);
                    files[1].CopyTo(fileStream2);
                    // file.CopyTo(fileStream);
                    model.CustImageThumbnailUrl = Passport;
                    model.ScannThumbnailUrl = Signature;

                }
                var result = await _loanRepo.CreateLoan(model, AccountNumber, LoginUser, userId);

                if(result != null)
                {
                        var subjectTo = "ALERT NOTIFICATION";

                        var messages = "</br><b> Dear </b>" + result.recepientName;
                        messages += ("<br />");
                        messages += "</br><b> Your account number: </b>" + AccountNumber;
                        messages += ("<br />");
                        messages += "</br><b> Loan amount given: #</b>" + result.AmountGiven;
                        messages += ("<br />");
                        messages += "</br><b> Total amount to pay: #</b>" + result.Amount;
                        messages += ("<br />");
                        messages += "</br><b> Interest Rate: </b>" + result.Interest_Rate+"%";
                        messages += ("<br />");
                        messages += "</br><b> loan return: </b>" + result.Frequency;
                        messages += ("<br />");
                        messages += "</br><b> Loan due month: </b>" + result.Deadline;
                        messages += ("<br />");
                        messages += "</br><b> Date registered: </b>" + result.Date;
                        messages += ("<br />");
                        messages += ("<br />");
                        messages += "</br>Regards";

                        await _emailSender.SendEmailAsync(result.recipientEmail, subjectTo, messages, "");

                        //alert message
                        TempData["Message"] = "Account number " + " " + AccountNumber + " " + " Successfully created";

                        dynamic transRef = TempData["Message"];

                        Alert("success", transRef, NotificationType.success);

                    return RedirectToAction("Index", "Home");
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


        [Authorize(Policy = Claims.Cashier)]
        public IActionResult ActivateLoan()
        {
            return View();
        }

        [HttpGet]
        public  IActionResult ReNewLoan(string accountNumber)
        {
            try
            { 
            var getAccountNo = _loanRepo.ActivateLoan(accountNumber);

            if (getAccountNo == null)
            {
                return RedirectToAction("SearchCustToActivate", "Customer");
            }
            ///////////////
            LoanModel smodel = _mapper.Map<LoanModel>(getAccountNo);

            return View(smodel);

            //return View(Model);
        }
            catch (Exception ex)
            {
                throw ex;
               // return View("Error");
            }
}
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = Claims.Cashier)]
        public async Task<IActionResult> ReNewLoan([FromForm] LoanModel model)
        {
            if (ModelState.IsValid)
            {

                var result = await _loanRepo.ReNewLoan(model);

                if (result)
                {
                    _ElasticEmailService = new ElasticEmailService();
                    string name = model.Surname + " " + model.GivenName;
                    await _ElasticEmailService.Send(model.EmailAdress, "account created sussefully", name + " " + model.AccountNumber);
                    //string emailSent = await _emailSending.Send(model.EmailAdress, "account created sussefully", name + " " + AccountNumber);

                    //alert message
                    TempData["Message"] = "Account number " + " " + model.AccountNumber + " " + " Successfully re-activated";

                    dynamic transRef = TempData["Message"];

                    Alert("success", transRef, NotificationType.success);

                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                TempData["Error"] = "bad";

            }
            return View(model);
        }
        [Authorize(Policy = Claims.Admin)]
        public IActionResult AllacTiveLoans()
        {
            var loan = _loanRepo.GetAllLoans();
            List<LoanModel> smodel = _mapper.Map<List<LoanModel>>(loan);
            return View(smodel);
        }
    }
}