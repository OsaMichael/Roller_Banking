using Roller.Repository;
using Roller.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roller.Web.Utility
{
    public interface IEmailSender: IDependencyRegister
    {
        // bool SendEmail(string recepientName, string recipientEmail, string accountNumber/*, int verificationCode*/);
        bool sendmail(string recepientName, string recipientEmail, string accountNumber/*, decimal amount*/);
        Task SendEmailAsync(string email, string subject, string message, string attachedfiles);
        // bool sendmail(string model);
        // bool sendmail(SendMialViewModel model);
    }
}
