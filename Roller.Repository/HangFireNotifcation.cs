using Hangfire;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Roller.Repository
{
    public class HangFireNotifcation
    {

        private readonly RequestDelegate next;
        //private readonly EmailLog emailLog;
        private static ILogger logger;
        public HangFireNotifcation(RequestDelegate next)
        {
            this.next = next;
        }
        public async Task Invoke(HttpContext context, INotification _notification, IMainNotification _mailEmail)
        {
            try
            {  // to run every 15 ninute
                //RecurringJob.AddOrUpdate(() => _notification.SendMaill(), "*/15 * * * *");
                //RecurringJob.AddOrUpdate(() => _mailEmail.SendMaill2(), "*/20 * * * *");
                  //RecurringJob.AddOrUpdate(() => _mailEmail.SendMaill2(), "*/20 * * * *");
                RecurringJob.AddOrUpdate(() => _notification.SendMaill(), Cron.Hourly);
                RecurringJob.AddOrUpdate(() => _mailEmail.SendMaill2(),Cron.Hourly);
                //RecurringJob.AddOrUpdate(() => Console.Write("Recurring"), "*/15 * * * *");
            }
            catch (Exception ex)
            {
            }
            await next(context);
        }
    }
}
