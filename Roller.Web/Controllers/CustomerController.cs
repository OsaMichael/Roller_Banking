using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using FluentValidation.Results;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Roller.DataContext.Entity;
using Roller.DContext;
using Roller.Repository;
using Roller.Repository.Customers;
using Roller.Repository.Extensions;
using Roller.Repository.TransactRepo;
using Microsoft.AspNetCore.Authorization;
using Roller.Web.Models;
using Roller.Web.Utility;
using AuthorizeAttribute = Microsoft.AspNetCore.Authorization.AuthorizeAttribute;

namespace Roller.Web.Controllers
{
    [Authorize]
    public class CustomerController : Controller
    {
        private ICustomerManager _custManager;
        private INotification _notification;
        private readonly IConfiguration _config;
        private readonly RollerDataContext _context;
        private readonly IMapper _mapper;
        private IHostingEnvironment _hostingEnvironment;
       // private readonly IElasticEmailService _Elastic;
        //private IEmailSender _emailSender;
        //private ILogger _logger { get; }

        public CustomerController(ICustomerManager custManager, IConfiguration config,
            RollerDataContext context, IMapper mapper, IHostingEnvironment hostingEnvironment , INotification notification)
        {
            _custManager = custManager;
            _context = context;
            _mapper = mapper;
            _hostingEnvironment = hostingEnvironment;
            _config = config;
            _notification = notification;
            //_logger = logger;
        }
        //[Authorize(Policy = Claims.Cashier)]
        // TO Deposit
        public IActionResult Index()
        {
            // search for customer for deposit
            return View();
        }
        // TO WITHDRAWL
        public IActionResult Index2()
        {
            // search for customer to withdraw
            return View();
        }
        public IActionResult Index3()
        {
            // get a customer full details
            return View();
        }
        public IActionResult ActivatLoan()
        {
            return View();
        }
        public async Task<IActionResult> Search(GetCustomerListQuery query)
        {
            query.Offset = query.Limit * (query.CurrentPage - 1);
            return View( await _custManager.GetCustomerList(query));
        }
        [Authorize(Policy = Claims.Cashier)]
        public async Task<IActionResult> SearchWithdraw(GetCustomerListQuery query)
        {
            query.Offset = query.Limit * (query.CurrentPage - 1);
            return View(await _custManager.GetCustomerList(query));
        }
        [Authorize(Policy = Claims.Cashier)]
       // [Authorize(Roles  = Claims.Admin)]
        public async Task<IActionResult> SearchFullCustDetails(GetCustomerListQuery query)
        {
            query.Offset = query.Limit * (query.CurrentPage - 1);
            //return View(await _custManager.GetCustomerList(query));
            var result =   await _custManager.GetCustomerList(query);
            return View(result);
        }
        [Authorize(Policy = Claims.Cashier)]
        public IActionResult SearchCustToActivate(GetCustomerListQuery query)
        {
            query.Offset = query.Limit * (query.CurrentPage - 1);
            //return View(await _custManager.GetCustomerList(query));
            var result =  _custManager.GetCustomerAndLoanList(query);
            return View(result);
        }
        public async Task<IActionResult> SearchCustToTransferFrom(GetCustomerListQuery query)
        {
            query.Offset = query.Limit * (query.CurrentPage - 1);
            //return View(await _custManager.GetCustomerList(query));
            var result = await _custManager.GetCustomerList(query);
            return View(result);
        }

   
        //////////////////////////////////////////////////////////////////////////////////////
        //[Authorize(Policy = Claims.User)]
        //public async Task<IActionResult> ActivateLoan(GetCustomerListQuery query)
        //{


        //    //query.Offset = query.Limit * (query.CurrentPage - 1);
        //    ////return View(await _custManager.GetCustomerList(query));
        //    var result = await _custManager.GetCustomerList(query);
        //    return View();
        //}


        public async Task<IActionResult> TransactionDetails(GetCustomerListQuery query)
        {
            query.Offset = query.Limit * (query.CurrentPage - 1);
            return View(await _custManager.GetCustomerList(query));
        }

        [System.Web.Http.HttpGet]
        //[Authorize(Policy = Claims.Admin)]
        public ActionResult Transactions(string accountNumber)
        {
            var result = _custManager.GetTransactions(accountNumber);

            List<TransactionDto> smodel = _mapper.Map<List<TransactionDto>>(result);


            return View(smodel);
        }
        public async Task <IActionResult> CustomerDetails(string accountNumber)
        {
            try
            {
                //var result = _custManager.GetCustomID(id);
                var result = await _custManager.GetCustomerDetails(accountNumber);


                CustomerDto mappedUser = _mapper.Map<CustomerDto>(result);
              
                return View(mappedUser);
                //return View(await Mediator.Send(new GetCustomerDetailsQuery { CustomerId = id }));
            }
            catch (NotFoundException)
            {
                TempData["Error"] = $"Customer ID {accountNumber} not found.";
            }

            return RedirectToAction(nameof(Index), "Home");
        }
        [System.Web.Http.HttpPost]
        public async Task<IActionResult> CustomerToWithDral(string accountNumber)
        {
            try
            {
                //var result = _custManager.GetCustomID(id);
                var result = await _custManager.GetCustomerDetails(accountNumber);
                var account = _context.Accounts.SingleOrDefault(b => b.AccountNumber == accountNumber);
                if (result.AccountNumber != account.AccountNumber) throw new Exception("invalid account number");

                ViewBag.info = account;
                ViewBag.balance = account.Balance;
                ViewBag.accountNumber = account.AccountNumber;

                CustomerDto mappedUser = _mapper.Map<CustomerDto>(result);

                return View(mappedUser);
                //return View(await Mediator.Send(new GetCustomerDetailsQuery { CustomerId = id }));
            }
            catch (NotFoundException)
            {
                TempData["Error"] = $"Customer ID {accountNumber} not found.";
            }

            return RedirectToAction(nameof(Index), "Home");
        }

     
       

        public IActionResult ShowBalace()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [System.Web.Http.HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize(Policy = Claims.User)]
        public async Task< IActionResult> Register([FromForm]CustomerProfile model)
        {
            //CustomerAccountValidator customerAccountValidator = new CustomerAccountValidator();
            //ValidationResult resultt = customerAccountValidator.Validate(model);

            //if (!resultt.IsValid)
            //{
            //    return BadRequest(resultt.Errors);
            //}
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
                //foreach (var Image in files)
                //{
                    //if (Image != null && Image.Length > 0)
                    //{
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
                    //}
                    //}
                }
                var result = await _custManager.CreateCustomer(model, AccountNumber, LoginUser);

                if ( result)
               {
                    //TODO: Send Email with AccountNumber And Verification Code
                    string name = model.Surname + " " + model.GivenName;
                   // string emailSent = await _notification.Send(model.EmailAdress, "account created sussefully", name + " " + AccountNumber);


                    TempData["Message"] = "";
                    return RedirectToAction("Index","Home");
               }
                else
                {
                    TempData["Error"] = "bad";
                    
                }

            }
            //if (ModelState.IsValid)
            //{
            //var result = _custManager.CreateCustomer(query);
            //   // var result = await Mediator.Send(query);
            //    if (result != null)
            //    {
            //        TempData["Message"] = result.Status;
            //        return RedirectToAction("Home","Index");
            //    }
            //  //  TempData["Error"] = result.Error;
            ////}
            return View(model);
        }
    }
}