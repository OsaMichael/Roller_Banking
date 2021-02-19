using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Roller.Repository.CashierRepo;
using Roller.Repository.Customers;
using Roller.Repository.Extensions;
using Roller.Repository.Services;

namespace Roller.Web.Areas.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _payment;
        private readonly ICustomerManager _custManager;

        public PaymentController(IPaymentService payment, ICustomerManager custManager)
        {
            _custManager = custManager;
            _payment = payment;
        }
        private string GenerateMerchantRef()
        {
            var generatePayReference = new Random();
            string referencecode = generatePayReference.Next(12321232).ToString();
            return referencecode;
        }

        [HttpPost("transferBalance")]
        public IActionResult TransferBalance([FromForm] TransferModel model)
        {
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

                var postTransction = _payment.PostTransaction(gotoPaymentDirect);
                if (postTransction.UniqueKey != null)
                    return Ok(model.ToResponse());/*Json(new { redirectUrl = postTransction.UniqueKey, isRedirect = true }, jsonRequestBehavior);*/


                if (gotoPaymentDirect.Amount != 0)
                {

                    return RedirectToAction("MakePayment", gotoPaymentDirect);
                }
                else
                {
                    return RedirectToAction("MakePayment", gotoPaymentDirect);
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
            //else
            //{
            //    TempData["Error"] = "bad";

            //}
            return BadRequest();
        }


        // GET: api/Payment
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Payment/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Payment
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Payment/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
