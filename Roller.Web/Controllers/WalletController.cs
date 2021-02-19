using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Roller.Repository.CashierRepo;
using Roller.Repository.Customers;
using Roller.Repository.Services;

namespace Roller.Web.Controllers
{
    public class WalletController : BaseController
    {
        private ICustomerManager _custManager;
        private readonly IPaymentService _payment;
        public WalletController(ICustomerManager custManager, IPaymentService payment)
        {
            _custManager = custManager;
            _payment = payment;
        }
        public IActionResult Index()
        {
            var use = User.Identity.Name;

            var load = _custManager.GetCustomEmailwithUserSignin(use);

            WalletDto walletDto = new WalletDto();
            walletDto.AccountBalance = load.Cust_balance;
            walletDto.AccountNumber = load.CustAccNumber;

            return View(walletDto);
        }

        [HttpGet]
        public IActionResult AddWallet(string accountNumber)
        {
            WalletDto walletDto = new WalletDto();
            walletDto.AccountNumber = accountNumber;
            return View(walletDto);
        }

        [HttpPost]
        public IActionResult AddWallet(WalletDto wallet)
        {
            /// here take it to payment gateway

            //TransferValidator bankValidator = new TransferValidator();
            //ValidationResult result = bankValidator.Validate(wallet);

            //if (!result.IsValid)
            //{
            //    return BadRequest(result.Errors);
            //}

            if (ModelState.IsValid)
            {
                var cust = _custManager.GetCustomerDetails(wallet.AccountNumber);
                var jsonRequestBehavior = new JsonSerializerSettings();
                TransactionModel gotoPaymentDirect = new TransactionModel
                {
                    FullName = cust.Result.Surname + " " + cust.Result.GivenName,
                    CustomerID = cust.Result.CustomerId.ToString(),
                    Amount = wallet.Amount,
                    Savecode = GenerateMerchantRef(),
                    Email = cust.Result.EmailAdress,
                    Phone = cust.Result.TelephoneNumber,
                    IsActive = false,
                    IsDeleted = false

                };

                var postTransction = _payment.PostTransaction(gotoPaymentDirect);
                if (postTransction.UniqueKey != null)
                    return Json(new { redirectUrl = postTransction.UniqueKey, isRedirect = true }, jsonRequestBehavior);


                if (gotoPaymentDirect.Amount != 0)
                {

                    return RedirectToAction("MakePayment", gotoPaymentDirect);
                }
                else
                {
                    return View("MakePayment", gotoPaymentDirect);
                }

            }
            else
            {
                TempData["Error"] = "bad";

            }
            return View(wallet);
        }
        private string GenerateMerchantRef()
        {
            var generatePayReference = new Random();
            string referencecode = generatePayReference.Next(12321232).ToString();
            return referencecode;
        }

    }
}