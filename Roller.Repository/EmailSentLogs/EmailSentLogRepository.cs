using Microsoft.EntityFrameworkCore;
using Roller.DataContext.Entity;
using Roller.DContext;
using Roller.Repository.EmailSentLogs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Roller.Repository
{
   public class EmailSentLogRepository: IEmailSentLog
    {
        private readonly RollerDataContext _context;

        public EmailSentLogRepository(RollerDataContext context)
        {
            _context = context;
        }

        public async Task<bool> LogEmailAsync(EmailSentLog emailLog)
        {
            if (emailLog != null)
            {

                await _context.AddAsync(emailLog);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> LogEmailTransactionAsync(EmailSentLog emailLog)
        {
            if (emailLog != null)
            {

                await _context.AddAsync(emailLog);
                //await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> UpdateEmailAsync(EmailSentLog emailLog)
        {
            if (emailLog != null)
            {
                _context.Update(emailLog);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<EmailSentLog> GetEmailSentByIdAsync(int Id)
        {
            return await _context.EmailSentLogs.FindAsync(Id);
        }
        public async Task<IEnumerable<EmailSentLog>> GetEmailSentLogAsync()
        {
            return await _context.EmailSentLogs.ToListAsync();
        }
    }
}
