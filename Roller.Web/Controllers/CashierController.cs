using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Roller.DataContext.Entity;
using Roller.DContext;
using Roller.Repository.CashierRepo;
using Roller.Repository.Customers;
using Roller.Repository.Services;
using Roller.Repository.TransactRepo;
using Roller.Web.Models;
using Roller.Web.Utility;
using static Roller.Web.Controllers.Common.Enum;

namespace Roller.Web.Controllers
{
    public class CashierController : BaseController
    {
        private readonly ICustomerManager _custManager;
        private readonly ITransManager _transManager;
        private readonly IMapper _mapper;
        private readonly ICashierRepository _cashier;
        private readonly RollerDataContext _context;
        private IEmailSender _emailSender;
        private readonly IPaymentService _payment;

        public CashierController(ICustomerManager custManager, ITransManager transManager,
            IMapper mapper, RollerDataContext context, ICashierRepository cashier,
            IEmailSender emailSender, IPaymentService payment)
        {
            _custManager = custManager;
            _transManager = transManager;
            _mapper = mapper;
            _context = context;
            _cashier = cashier;
            _emailSender = emailSender;
            _payment = payment;
        }
        public IActionResult Index()
        {
            return View();
        }
      

        [HttpGet]
        public ActionResult CashWithdraw()
        {
            return View();
        }
        // TO WITHDRAW FROM SAME BANK
        [HttpPost, ActionName("CashWithdraw")]
        public ActionResult ConfirmCashWithdraw(ShowBalanceDto model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                        string LoginUser = User.Identity.Name;
                        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                        var result = _custManager.GetWildrawal(model, LoginUser);

                        if (result != null)
                        {
                        var subject = "ALERT NOTIFICATION";

                        var message = "</br><b> Dear </b>" + result.FullName;
                        message += ("<br />");
                        message += "</br><b> Your account: </b>" + result.AccountNumber;
                        message += ("<br />");
                        message += "</br><b> Debited with: #</b>" + model.Amount;
                        message += ("<br />");
                        message += "</br><b> Date of Transaction: #</b>" + result.Date;
                        message += ("<br />");
                        message += ("<br />");
                        //message += "</br>has been registered successful on Cyberspace E-procurement Portal.</br>";
                        //message += "</br>Kindly, log in via " + requisitionURL + " and validate the required documents.";
                        message += "</br>Regards";

                        _emailSender.SendEmailAsync(result.RecipientEmail, subject, message, "");
                        //////// message

                        ViewBag.account = result.AccountNumber;
                        ViewBag.balance = result.Balance;
                        ViewBag.imageThumb = result.CustImageThumbnailUrl;


                        TempData["Message"] = "Copy this number " + " " + result.DateAndTransId + " " + "Withdraw Successfull";

                        dynamic transRef = TempData["Message"];

                        Alert("success", transRef, NotificationType.success);

                        return View("ShowWithdrawBalance");
                        }
                        else
                        {
                        Alert("success", " ", NotificationType.success);
                       // ViewData["Message"] = "Amount Is higher than available balance/Invalid";

                        }
                        //  return RedirectToAction("Index", "Category");                
                }
                return View(model);
            }
            catch (Exception ex)
            {

                throw ex;
            }
                
           // return View("Empty");

            
        }
        public IActionResult ShowWithdrawBalance()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CashDeposit()
        
{
            return View();
        }

        [HttpPost, ActionName("CashDeposit")]
        public ActionResult ConfirmCashDeposit(ShowBalanceDto model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string LoginUser = User.Identity.Name;
                    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                    var result = _custManager.GetDeposit(model, LoginUser);

                    if (result != null)
                    {
                        var subject = "ALERT NOTIFICATION";

                        var message = "</br><b> Dear </b>" + result.FullName;
                        message += ("<br />");
                        message += "</br><b> Your account: </b>" + result.AccountNumber;
                        message += ("<br />");
                        message += "</br><b> Credited with: #</b>" + model.Amount;
                        message += ("<br />");
                        message += "</br><b> Date of Transaction: #</b>" + result.Date;
                        message += ("<br />");
                        message += ("<br />");
                        //message += "</br>has been registered successful on Cyberspace E-procurement Portal.</br>";
                        //message += "</br>Kindly, log in via " + requisitionURL + " and validate the required documents.";
                        message += "</br>Regards";

                        _emailSender.SendEmailAsync(result.RecipientEmail, subject, message, "");

                        //alert pop up here
                        ViewBag.account = result.AccountNumber;
                        ViewBag.balance = result.Balance;
                        ViewBag.transId = result.DateAndTransId;
                         
                        TempData["Message"] = "Copy this number "+ " " + result.DateAndTransId  + " " + "Deposit Successfull";

                        dynamic transRef = TempData["Message"];

                        Alert("success", transRef, NotificationType.success);/*as AlertMessage;*/

                        return View("ShowBalance");

                    }
                }
                else
                {
                    Alert("success", " ", NotificationType.success);/*as AlertMessage;*/
                    //ViewData["Message"] = "Amount has to be higher that 1000Tk/Invalid";
                }
                    return View(model);
            }

            catch (Exception ex)
            {

                throw ex;
            }
        }

        public IActionResult ShowBalance ()
        {
            return View();
        }
        public ActionResult CashierTransfer()
        {

            return View();
        }
        private string GenerateMerchantRef()
        {
            var generatePayReference = new Random();
            string referencecode = generatePayReference.Next(12321232).ToString();
            return referencecode;
        }

        [HttpGet]
        public ActionResult TransferBalance(string accountNumber)
        {
            var acctNo = _cashier.GetCustToTransfFrm(accountNumber);
            if (acctNo == null)
            {
                return RedirectToAction("SearchCustToTransferFrom", "Customer");
            }
            ///////////////
            TransferModel smodel = _mapper.Map<TransferModel>(acctNo);

            return View(smodel);
        }

        // Using cyberpay as gatway

        [HttpPost, ActionName("TransferBalance")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = Claims.Cashier)]
        // public IActionResult ConfirmTransferBalance([FromForm] TransferModel model)
        public IActionResult ConfirmTransferBalance([FromForm] TransferModel model)
        {
           // TransferValidator
            TransferValidator bankValidator = new TransferValidator();
            ValidationResult result = bankValidator.Validate(model);

            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }

            if (ModelState.IsValid)
            {
                var cust = _custManager.GetCustomerDetails(model.AccountNumber_from);
                var jsonRequestBehavior = new JsonSerializerSettings();
                Repository.Services.TransactionModel gotoPaymentDirect = new Repository.Services.TransactionModel
                {
                    FullName = cust.Result.Surname + " " + cust.Result.GivenName,
                    CustomerID = cust.Result.CustomerId.ToString(),
                    Amount = model.Amount,
                    Savecode = GenerateMerchantRef(),
                     Email = cust.Result.EmailAdress,
                     Phone = cust.Result.TelephoneNumber,
                     IsActive = false,
                     IsDeleted = false

                };
                     
                var postTransction =  _payment.PostTransaction(gotoPaymentDirect);
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

                //  var result = await _cashier.TransferBalance(model);
                //if (result != null)
                //{
                //    var subjectFrom = "ALERT NOTIFICATION";

                //    var message = "</br><b> Dear </b>" + result.recepientName;
                //    message += ("<br />");
                //    message += "</br><b> Your account: </b>" + result.accountNumber;
                //    message += ("<br />");
                //    message += "</br><b> Debited with: #</b>" + model.Amount;
                //    message += ("<br />");
                //    message += "</br><b> Date of Transaction: #</b>" + result.Date;
                //    message += ("<br />");
                //    //message += "</br><b> VERIFY CODE: </b>" + Generate.GenerateVerificationCode().ToString();
                //    //message += ("<br />");
                //    message += ("<br />");
                //    //message += "</br>has been registered successful on Cyberspace E-procurement Portal.</br>";
                //    //message += "</br>Kindly, log in via " + requisitionURL + " and validate the required documents.";
                //    message += "</br>Regards";

                //  await  _emailSender.SendEmailAsync(result.recipientEmail, subjectFrom, message, "");

                //    var subjectTo = "ALERT NOTIFICATION";

                //    var messages = "</br><b> Dear </b>" + result.recievedMails.recipientEmail;
                //    messages += ("<br />");
                //    messages += "</br><b> Your account: </b>" + result.recievedMails.accountNumber;
                //    messages += ("<br />");
                //    messages += "</br><b> Credited with: #</b>" + model.Amount;
                //    messages += ("<br />");
                //    messages += "</br><b> Date of Transaction: #</b>" + result.recievedMails.Date;
                //    messages += ("<br />");
                //    messages += ("<br />");
                //    messages += "</br>Regards";

                //    await _emailSender.SendEmailAsync(result.recipientEmail, subjectTo, messages, "");

                //string acountN0 = result.accountNumber;
                //TODO: Send Email with AccountNumber And Verification Code
                //string nameTransFrom = result.recepientName;
                //bool emailSent = _emailSender.sendmail(result.recipientEmail, nameTransFrom , model.Amount +" " + "Debited from account No." + " " + result.accountNumber );//* Generate.GenerateVerificationCode()*/);

                //string nameTransTo = result.recievedMails.recepientName;
                //bool emailSentR = _emailSender.sendmail(result.recievedMails.recipientEmail, nameTransTo, model.Amount.ToString() +" " + "Credited to account No." + " " + result.recievedMails.accountNumber/*, result.Amount*//* Generate.GenerateVerificationCode()*/);

                //alert message
                //TempData["Message"] = "Transaction Successful";

                //    dynamic transRef = TempData["Message"];

                //    Alert("success", transRef, NotificationType.success);

                //    return RedirectToAction("Index", "Home");
                //}
            }
            else
            {
                TempData["Error"] = "bad";

            }
            return View(model);
        }
    }
    }



//private Operation<PaymentModel> makepayment(PaymentModel payment, string itemname, string[] split, int category = 1)
//{
//    return Operation<PaymentModel>.Create(operation =>
//    {




//        payment.ItemName = "Wallet Top Up";
//        payment.PaymentType = PaymentTypeInfo.Wallet;
//        var payresp = _payment.CreditCustomerPayment(payment);
//        if (payresp.Status.Equals(StatusCode.Failed)) throw new Exception(payresp.Message);
//        payment = payresp.Result;


//        return payment;
//    });
//}

//public Operation<PaymentModel> CreditCustomerPayment(PaymentModel payment)
//{
//    //TODO: Complete Credit Customer
//    return Operation<PaymentModel>.Create(operation =>
//    {
//        if (payment.Amount <= 0) throw new Exception("Invalid Amount Received");


//        //Get the customers Info
//        var getCustomer = GetCustomerByID(new CustomerModel { i_customer = payment.Customer.i_customer }).Throw();
//        // if (getCustomer.Result.i_customer == payment.Customer.i_customer) payment.PaymentType = PaymentTypeInfo.Commission;
//        payment.OldBalance = getCustomer.Result.Balance.HasValue ? getCustomer.Result.Balance.Value : 0;

//        payment.Customer.Balance = (getCustomer.Result.Balance.HasValue ? getCustomer.Result.Balance.Value : 0) + payment.CreditedAmount;

//        payment.Customer.RequestFrom = payment.RequestFrom;
//        EditCustomer(payment.Customer);



//        var stri = payment.Description.Split('-')[1];
//        //generate receipt
//        string receiptContent = "";
//        var receiptdet = WalletGenerateReceipt(payment, "", loanstoprocess);
//        if (receiptdet.Status.Equals(StatusCode.Succeeded)) receiptContent = receiptdet.Result;

//        //send receipt and mail/sms to customer with ticket number
//        CustomerPayment model = new CustomerPayment
//        {
//            AmountPaid = payment.Amount,
//            CustomerID = payment.Customer.i_customer,
//            CustomerName = payment.Customer.FirstName,
//            ReceiptContent = receiptContent,
//            ReceiptNumber = payment.ReceiptNumber,
//            To = payment.Customer.EmailAddress,
//            Phone = !string.IsNullOrWhiteSpace(payment.Customer.MobilePhone) ?
//            payment.Customer.MobilePhone : payment.Customer.HomePhone,
//            WalletBalance = payment.Customer.Balance.HasValue ?
//            payment.Customer.Balance.Value : 0,
//            OperatorCode = payment.Customer.OperatorCode,
//            EmailUnsubscribe = payment.Customer.EmailUnsubscribe.HasValue ? payment.Customer.EmailUnsubscribe.Value : false,
//            SMSUnsubscribe = payment.Customer.SMSUnsubscribe.HasValue ? payment.Customer.SMSUnsubscribe.Value : false
//        };

//        var sendmail = _emailMgr.SendEmail(model);
//        if (sendmail.Status == Library.StatusCode.Failed)
//        {
//            sendmail.Log("Modules.Managers.UtilityService.CreditCustomerPayment", sendmail.Error);
//            payment.IsEmailSent = false;
//        }
//        else
//        {
//            payment.IsEmailSent = sendmail.Result;
//        }

//        var sendsms = _sms.SendSMS(model);
//        if (sendsms.Status == Library.StatusCode.Failed)
//        {
//            sendmail.Log("Modules.Managers.UtilityService.CreditCustomerPayment", sendsms.Error);
//            payment.IsSMSSent = false;
//        }
//        else
//        {
//            payment.IsSMSSent = sendsms.Result;
//        }

//        _crmpayprocess.EditPayment(payment);

//        return payment;
//    });
//}