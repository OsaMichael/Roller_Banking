//using AutoMapper.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Roller.DataContext.Entity;
using Roller.DContext;
using System.Security.Claims;
using Roller.Web.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Threading;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using CAIRO.ElasticEmail;
using Hangfire;
using System.Diagnostics;

namespace Roller.Repository
{
    public class Notification : INotification
    {
        private readonly EmailSettings _emailSettings;
        private RollerDataContext _context;
        private readonly IConfiguration _configuration;
        public Notification(IConfiguration configuration, IOptions<EmailSettings> emailSettings,
            RollerDataContext context /*,IMainNotification mainEmail*//*IBackgroundJobClient backgroundJob*/)
        {
            _context = context;
            _emailSettings = emailSettings.Value;
            _configuration = configuration;
        }
        public async Task SendMaill()        {
            //DateTime.Now.AddDays(-1);
            //Transaction trans = new Transaction();
            var activeLoan = _context.Loans.Where(c => c.IsActive == true).ToList();

            _emailSettings.FromName = _configuration.GetValue<string>("ElasticEmail:FromName");
            _emailSettings.From = _configuration.GetValue<string>("ElasticEmail:From");
            _emailSettings.ApiKey = _configuration.GetValue<string>("ElasticEmail:ApiKey");

            SupperAccount amount = _context.SupperAccounts.First(c => c.SupperAcctNumber == "1234567890");

            if (activeLoan.Any())
            {
            var dict = new Dictionary<string, string>
               {
                { "apikey", _emailSettings.ApiKey },
                { "from", _emailSettings.From },
                { "fromName", _emailSettings.FromName },
                { "isTransactional", "true" }
               };

               foreach (var item in activeLoan)
                {
                    Transaction trans = new Transaction();
                    var ifExists = _context.SupperAccounts.SingleOrDefault(c => c.LoanId == item.LoanId && c.TransactDate == DateTime.Now.ToString("yyyy-MM-dd"));
                   // var ifExists = _context.SupperAccounts.SingleOrDefault(c => c.LoanId == item.LoanId && c.TransactDate == DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd"));
                    // var ifExists = _context.SupperAccounts.SingleOrDefault(c => c.LoanId == item.LoanId && trans.Tr_Date ==  DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd"));
                    if (ifExists == null)
                    {
                       // SupperAccount amount = _context.SupperAccounts.First(c => c.SupperAcctNumber == "1234567890");

                        decimal addAmount = 0;
                        SupperAccount supper = new SupperAccount();
                       // Transaction trans = new Transaction();

                        if (item.Loan_Amount_Paid >= 4000)
                        {
                            if (item.Loan_Amount_Paid > 4000)
                            {
                                item.Loan_Amount_Paid = item.Loan_Amount_Paid - 4000;
                                addAmount = 4000;

                                // doing this will update a single role 
                                // amount.Amount = amount.Amount + Convert.ToDecimal(addAmount);

                                //if (supper.LoanId == 0 /*&& supper.TransactDate == DateTime.Now.ToString("yyyy-MM-dd")*/)
                                //{
                                //    //doing this will update on a different role for each individual
                                supper.Amount = supper.Amount + Convert.ToDecimal(addAmount);
                                supper.SupperAcctNumber = amount.SupperAcctNumber;
                                supper.SupperPhone = amount.SupperPhone;
                                supper.SupperEmail = amount.SupperEmail;
                                supper.LoanId = item.LoanId;
                                supper.TransactDate = DateTime.Now.ToString("yyyy-MM-dd");
                                // supper.CustAcctNumber = item.AccountNumber;

                                _context.SupperAccounts.Add(supper);
                            }
                            else if (item.Loan_Amount_Paid == 4000)
                            {
                                supper.Amount = 0;
                                supper.LoanId = 0;

                                supper.Amount = supper.Amount + Convert.ToDecimal(item.Loan_Amount_Paid);
                                supper.SupperAcctNumber = amount.SupperAcctNumber;
                                supper.SupperEmail = amount.SupperEmail;
                                supper.SupperPhone = amount.SupperPhone;
                                supper.LoanId = item.LoanId;
                                supper.TransactDate = DateTime.Now.ToString("yyyy-MM-dd");
                                // supper.TransactDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                                //supper.TransactDate = DateTime.Now.ToString("hh:mm tt");

                                item.Loan_Amount_Paid = item.Loan_Amount_Paid - 4000;

                                //Transaction trans = new Transaction();
                                //trans.Amount = item.Loan_Amount_Paid;
                                ////trans.Amount = addAmount;
                                //trans.AccountNumber = item.AccountNumber;
                                //trans.Date = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
                                //trans.Type = "Admin";

                                //_context.Transactions.Add(trans);

                                _context.SupperAccounts.Add(supper);
                                //await _context.Saved();
                            }
                            else
                            {
                                throw new Exception("Amount Invalid");
                            }
                        }
                        else if (item.Loan_Amount_Paid < 4000)
                        {
                            // break;
                            // if the account is zero when the customer is active
                        }
                        await _context.Saved();

                        var mbody = new ElasticemailMessage();

                    dict.TryAdd("to", amount.SupperEmail);
                    mbody.Subject = "creaded loan";
                    dict.TryAdd("amount", Convert.ToString(amount.Amount));
                    dict.TryAdd("accountNumber", amount.CustAcctNumber);
                    mbody.IsBodyHtml = true;

                        //string address = "https://api.elasticemail.com/v2/email/send";
                        string address = _emailSettings.Url;

                    using (HttpClient client = new HttpClient())
                    {
                        var formContent = new FormUrlEncodedContent(dict);
                        var apiResponse = await client.PostAsync(address, formContent);
                        apiResponse.EnsureSuccessStatusCode();
                        if (apiResponse != null)
                        {
                            // item.IsSent = true;

                        }
                        // await _context.Saved();
                        await _context.Saved();
                        await apiResponse.Content.ReadAsStringAsync();

                    }


                }
            }
          }
        }

    }
}
//public async Task InvokeAsync(HttpContext context)
//{
//    if (context.Request.Headers.ContainsKey("X-Correlation-ID"))
//    {
//        context.TraceIdentifier = context.Request.Headers["X-Correlation-ID"];
//        // WORKAROUND: On ASP.NET Core 2.2.1 we need to re-store in AsyncLocal the new TraceId, HttpContext Pair
//        _httpContextAccessor.HttpContext = context;
//    }

//    // Call the next delegate/middleware in the pipeline
//    await _next(context);
//}

//        public async Task<string> Send(string to, string subject, string content)
//{
//    //var penDingMail = _context.EmailLogs.Where(c => !c.IsSent).ToList();
// //   var penDingMail = _context.Customers.Where(c => !c.IsSent).ToList();

//    _emailSettings.FromName = _configuration.GetValue<string>("ElasticEmail:FromName");
//    _emailSettings.From = _configuration.GetValue<string>("ElasticEmail:From");
//    _emailSettings.ApiKey = _configuration.GetValue<string>("ElasticEmail:ApiKey");

//    var dict = new Dictionary<string, string>
//    {
//        { "apikey", _emailSettings.ApiKey },
//        { "from", _emailSettings.From },
//        { "fromName",_emailSettings.FromName },
//        { "to", to },
//        { "subject", subject },
//        { "bodyHtml", content },
//        { "isTransactional", "true" }
//    };

//   // var values = dict.Select(v => new KeyValuePair<string, string>(v.Key, v.Value));

//    string address = _emailSettings.Url;// "https://api.elasticemail.com/v2/email/send"; //_emailSettings.Url;

//    using (HttpClient client = new HttpClient())
//    {
//        //var formContent = new FormUrlEncodedContent(dict);
//        var formContent = new FormUrlEncodedContent(dict);
//        var apiResponse = await client.PostAsync(address, formContent);
//        apiResponse.EnsureSuccessStatusCode();
//        var response = await apiResponse.Content.ReadAsStringAsync();
//        return response;
//    }
//}
