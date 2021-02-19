using Roller.DataContext.Entity;
using Roller.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Roller.Repository.EmailSentLogs
{
   public interface IEmailSentLog : IDependencyRegister
    {
        Task<bool> LogEmailAsync(EmailSentLog emailLog);
        Task<bool> LogEmailTransactionAsync(EmailSentLog emailLog);

        Task<bool> UpdateEmailAsync(EmailSentLog emailLog);

        Task<EmailSentLog> GetEmailSentByIdAsync(int Id);

        Task<IEnumerable<EmailSentLog>> GetEmailSentLogAsync();
    }
}
