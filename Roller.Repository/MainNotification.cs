using Roller.DContext;
using Roller.Web.Utility;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using System.Net.Http;
using Roller.DataContext.Entity;
using System.Linq;
using CAIRO.ElasticEmail;

namespace Roller.Repository
{
   public class MainNotification: IMainNotification
    {
        private RollerDataContext _context;
        private readonly EmailSettings _emailSettings;
        private readonly IConfiguration _configuration;
        public MainNotification(IConfiguration configuration, IOptions<EmailSettings> emailSettings, RollerDataContext context)
        {
            _configuration = configuration;
            _context = context;
            _emailSettings = emailSettings.Value;
        }
        public async Task SendMaill2()
        {
            SupperAccount amount = _context.SupperAccounts.First(c => c.SupperAcctNumber == "1234567890");

            _emailSettings.FromName = _configuration.GetValue<string>("ElasticEmail:FromName");
            _emailSettings.From = _configuration.GetValue<string>("ElasticEmail:From");
            _emailSettings.ApiKey = _configuration.GetValue<string>("ElasticEmail:ApiKey");

            //if (activeLoan.Any())
            //{
                var dict = new Dictionary<string, string>
               {

                { "apikey", _emailSettings.ApiKey },
                { "from", _emailSettings.From },
                { "fromName", _emailSettings.FromName },
                { "isTransactional", "true" }
               };

            //var sumAmount = _context.SupperAccounts.Where(x => x.TransactDate == DateTime.Now.ToString("yyyy-MM-dd")).Sum(c => c.Amount);

            var sumAmount = _context.SupperAccounts.Where(x => x.TransactDate == DateTime.Now.ToString("yyyy-MM-dd"));

            var mbody = new ElasticemailMessage();

            dict.TryAdd("to", amount.SupperEmail);
            mbody.Subject = "Loan Credited";
            dict.TryAdd("amount", sumAmount.ToString());
            //dict.TryAdd("accountNumber", item.AccountNumber.ToString());
            dict.TryAdd("transactDate", amount.TransactDate.ToString());
            mbody.IsBodyHtml = true;
            dict.TryAdd("bodyHtml", "Roller Loan Credited");

            if (sumAmount != null)
            {
                string address = "https://api.elasticemail.com/v2/email/send";

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

