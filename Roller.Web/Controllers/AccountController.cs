using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using Roller.DataContext.Entity;
using Roller.DContext;
using Roller.Repository.AccountRepo;
using Roller.Repository.Customers;
using Roller.Repository.Extensions;
using Roller.Repository.LoanRepos;
using Roller.Web.Models;
using Roller.Web.Utility;
using static Roller.Web.Controllers.Common.Enum;

namespace Roller.Web.Controllers
{
    //[ApiController]
    public class AccountController : BaseController
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;
        private readonly IAccountManager _accountManager;
       private readonly ICustomerManager _custManager;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;
        private SignInManager<User> _signInManager;
        private readonly RollerDataContext _context;
        private IEmailSender _emailSender;
        private ILoanRepository _loanRepo;
        private IHostingEnvironment _hostingEnvironment;
        public AccountController(IMapper mapper, UserManager<User> userManager, RoleManager<Role>
            roleManager, IAccountManager accountManager, SignInManager<User> signInManager, 
            IHttpContextAccessor httpContextAccessor, ICustomerManager custManager,
            RollerDataContext context,   IEmailSender emailSender, 
            ILoanRepository loanRepo, IHostingEnvironment hostingEnvironment)
        {
            _mapper = mapper;
            _accountManager = accountManager;
            _roleManager = roleManager;
            _userManager = userManager;
            _signInManager = signInManager;
            _contextAccessor = httpContextAccessor;
            _context = context;
            _emailSender = emailSender;
            _custManager = custManager;
            _hostingEnvironment = hostingEnvironment;
            _loanRepo = loanRepo;
        }

        [AllowAnonymous]
        public IActionResult Login(string returnUrl = "")
        {
            var login = new LoginViewModel();
            login.ReturnUrl = returnUrl;
            return View(login);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel login)
        {
            if (ModelState.IsValid)
            {
                User appUser = await _userManager.FindByEmailAsync(login.Email);
                //var appU = await _userManager.GetClaimsAsync(appUser);
                if (appUser == null)
                {
                    throw new Exception("Email not found");
                }
                //var result = await _signInManager.PasswordSignInAsync(appUser, login.Password, false, false);
                var result = await _signInManager.PasswordSignInAsync(appUser, login.Password, login.RememberMe, lockoutOnFailure: true);
                //var result = await _signInManager.PasswordSignInAsync(appUser, login.Password, false, false);
                if (result != null)
                {

                     var claim = await _userManager.GetClaimsAsync(appUser);

                    return RedirectToAction("GetSignView", "Home");

                }
                ModelState.AddModelError(nameof(login.Email), "Login Failed: Invalid Email or password");
            }
            return View(login);
        }

       [HttpGet]
        public IActionResult EMAIL_OTP()
        {
            return View(new OTPModel());
        }
        [HttpPost]
        public async Task<IActionResult> EMAIL_OTP(OTPModel model )
        {
            dynamic code = Generate.GenerateVerificationCode();

            if(ModelState.IsValid)
            {
                try
                {
                    OTPLog oTPLog = new OTPLog
                    {
                        OTP = code,
                        //Email = model.Email
                        
                    };
                    _context.OTPs.Add(oTPLog);
                    _context.SaveChanges();

                    var subjectTo = "VERIFICATION CODE";

                    var messages = "</br><b> Verification code </b>" + code;
                    messages += ("<br />");
                    messages += ("<br />");
                    messages += "</br>Regards";
                    await _emailSender.SendEmailAsync(model.Email, subjectTo, messages, "");

                    return RedirectToAction (nameof(OTPNumbers), "Account", model.Email);
                }
                catch (Exception xe)
                {
                    throw xe;
                }
            }
                           
            return View(model);
        }
       

        [HttpGet]
        public IActionResult OTPNumbers()
        {
            return View(new OTPModel());
        }
        [HttpPost]
        public IActionResult OTPNumbers(OTPModel model)
        {
            if(ModelState.IsValid)
            {
                var otp =  _context.OTPs.SingleOrDefault(x => x.OTP == model.OTP);
                if (otp == null) throw new Exception("OTP is not valid");

                else
                {
                    return RedirectToAction("RegisterLoan", "Loan", model.Email);
                    //return RedirectToAction("RegisterClient", "Account", model.Email);
                    
                }
            }                  
            return View(model);
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

                var user = new User
                {
                    Email = model.EmailAdress,
                    UserName = model.EmailAdress,
                };
                var isExistEmail = _context.Users.SingleOrDefault(x => x.Email == model.EmailAdress);

                if (isExistEmail != null) throw new Exception("Email already taken");

               // var result = await _userManager.CreateAsync(user, model.Password);

                var resultTrue = await _userManager.CreateAsync(user, model.Password);
                if (resultTrue.Succeeded)
                {
                    await _userManager.AddClaimAsync(user, new Claim(Claims.User, "true"));
                    dynamic code = Generate.GenerateVerificationCode();

                
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

                    if (result != null)
                    {
                        var subjectTo = "ALERT NOTIFICATION";

                        var messages = "</br><b> Dear </b>" + result.recepientName;
                        messages += ("<br />");
                        messages += "</br><b> Your account number: </b>" + AccountNumber;
                        messages += ("<br />");
                        messages += "</br><b>  Amount to collect: #</b>" + result.AmountGiven;
                        messages += ("<br />");
                        messages += "</br><b> Total amount to pay: #</b>" + result.Amount;
                        messages += ("<br />");
                        messages += "</br><b> Interest Rate: </b>" + result.Interest_Rate + "%";
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



        [HttpGet]
        public IActionResult RegisterClient()
        {
            return View(new CustomerProfile());
        }
        // to register as extenal bank users
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterClient(CustomerProfile model)
        {
            if (ModelState.IsValid)
            {
                string AccountNumber = Generate.GenerateAccountNumber();
                while (_custManager.GetAccountViaAccountNumber(AccountNumber) != null)
                {
                    AccountNumber = Generate.GenerateAccountNumber();
                }
                var user = new User
                {
                    Email = model.EmailAdress,
                    UserName = model.EmailAdress,
                };
                var isExistEmail = _context.Users.SingleOrDefault(x => x.Email == model.EmailAdress);

                if (isExistEmail != null) throw new Exception("Email already taken");

                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddClaimAsync(user, new Claim(Claims.User, "true"));
                    dynamic code  = Generate.GenerateVerificationCode();
                    Customer customer = new Customer
                    {
                        Streetaddress = model.Address.StreetAdress.ToFirstLetterUpper(),
                        Cust_balance = 0,
                        Telephonenumber = model.TelephoneNumber,
                        Surname = model.Surname.ToFirstLetterUpper(),
                        Givenname = model.GivenName.ToFirstLetterUpper(),
                        OtherNames = model.OtherNames.ToFirstLetterUpper(),
                        CustAccNumber = AccountNumber,
                        BVNNumber = model.BVNNumber,
                        Birthday = model.Birthday,
                        Cust_acc_type = model.AccountType.ToString(),
                        CreatedBy = user.Email,
                        DateCreated = DateTime.Now,
                        Country = model.Address.Country.ToFirstLetterUpper(),
                        City = model.Address.City.ToFirstLetterUpper(),
                        Emailaddress = model.EmailAdress.ToFirstLetterUpper(),
                        Gender = model.Gender,
                        IsActive = true
                    };
                    _context.Customers.Add(customer);

                    var account = new Account
                    {
                        Balance = 0m,
                        Created = DateTime.Now,
                        AccountNumber = AccountNumber,
                        CustId = customer.Id
                    };                 
                    _context.Accounts.Add(account);
                    //_context.SaveChanges();
                    var resultt = await _context.SaveChangesAsync();

                    var subjectTo = "ALERT NOTIFICATION";

                    var messages = "</br><b> Dear </b>" + customer.Surname + " " + customer.Givenname + " " + customer.OtherNames;
                    messages += ("<br />");
                    messages += "</br><b> Your new account number is: </b>" + AccountNumber;
                    messages += ("<br />");
                    messages += "</br><b> Verification code: </b>" + code;
                    messages += ("<br />");
                    messages += "</br><b> Date registered: </b>" + DateTime.Now;
                    messages += ("<br />");
                    messages += ("<br />");
                    messages += "</br>Regards";


                    await _emailSender.SendEmailAsync(user.Email, subjectTo, messages, "");

                    TempData["Message"] = "Account created " + " " + " Successfully,  account number sent to you mail   ";

                    dynamic transRef = TempData["Message"];

                    Alert("success", transRef, NotificationType.success);


                    return RedirectToAction("Login", "Account");

                }
               // TempData["Message"] = $"Successfully created the user {model.Email}";
               
            }

            //  TempData["Error"] = $"An Error has occured while creating the user {model.Email}, try using a new email or password.";
            return View(model);
        }

    public IActionResult Register()
        {
            return View(new RegisterViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize(Policy = Claims.Admin)]
        public async Task<IActionResult> Register([FromForm] RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    UserName = model.Username,
                    Email = model.Email,
                    Address = model.Address,
                    branch = model.branch,
                    DateOfBirth = DateTime.Now,
                    FirstName = model.FirstName,
                    PhoneNumber = model.PhoneNumber,
                    LastName = model.LastName,
                    MiddleName = model.MiddleName,
                    State = model.State,
                   
                };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    switch (model.SelectedRole)
                    {
                        case Claims.Admin:
                            await _userManager.AddClaimAsync(user, new Claim(Claims.Admin, "true"));
                            // _userManager.AddToRole(user.UserName, "User");
                            break;
                        case Claims.User:
                            await _userManager.AddClaimAsync(user, new Claim(Claims.User, "true"));
                            break;
                        case Claims.BranchManager:
                            await _userManager.AddClaimAsync(user, new Claim(Claims.BranchManager, "true"));
                            break;
                        case Claims.Cashier:
                            await _userManager.AddClaimAsync(user, new Claim(Claims.Cashier, "true"));
                            break;
                        case Claims.HROfficer:
                            await _userManager.AddClaimAsync(user, new Claim(Claims.HROfficer, "true"));
                            break;
                        case Claims.LoanOfficer:
                            await _userManager.AddClaimAsync(user, new Claim(Claims.LoanOfficer, "true"));
                            break;
                        case Claims.MDirector:
                            await _userManager.AddClaimAsync(user, new Claim(Claims.MDirector, "true"));
                            break;
                        case Claims.Officer:
                            await _userManager.AddClaimAsync(user, new Claim(Claims.Officer, "true"));
                            break;
                        case Claims.TransferOfficer:
                            await _userManager.AddClaimAsync(user, new Claim(Claims.TransferOfficer, "true"));
                            break;
                    }
                    TempData["Message"] = $"Successfully created the user {model.Email}";
                    return RedirectToAction(nameof(Register));
                }

                TempData["Error"] = $"An Error has occured while creating the user {model.Email}, try using a new email or password.";
            }
            return View(model);
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("SignOut", "Home");
        }
        /// <summary>
        /// ////////////////////////
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> AccessDenied()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult GoogleLogin()
        {
            string redirectUrl = Url.Action("GoogleResponse", "Account");
            var properties = _signInManager.ConfigureExternalAuthenticationProperties("Google", redirectUrl);
            return new ChallengeResult("Google", properties);
        }

        [AllowAnonymous]
        public async Task<IActionResult> GoogleResponse()
        {
            ExternalLoginInfo info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
                return RedirectToAction(nameof(Login));

            var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, false);
            string[] userInfo = { info.Principal.FindFirst(ClaimTypes.Name).Value, info.Principal.FindFirst(ClaimTypes.Email).Value };
            if (result.Succeeded)
                return View(userInfo);
            else
            {
                User user = new User
                {
                    Email = info.Principal.FindFirst(ClaimTypes.Email).Value,
                    UserName = info.Principal.FindFirst(ClaimTypes.Email).Value,
                    Salary = "2000"
                };

                IdentityResult identResult = await _userManager.CreateAsync(user);
                if (identResult.Succeeded)
                {
                    identResult = await _userManager.AddLoginAsync(user, info);
                    if (identResult.Succeeded)
                    {
                        await _signInManager.SignInAsync(user, false);
                        return View(userInfo);
                    }
                }
                return await AccessDenied();
            }
        }

        [AllowAnonymous]
        public IActionResult Denied()
        {
            HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
            return View();
        }
    }
}