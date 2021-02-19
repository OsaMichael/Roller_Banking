using Roller.DataContext.Entity;
using Roller.Repository.Interface;
using Roller.Web.Utility;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Roller.Repository.LoanRepos
{
    public interface ILoanRepository: IDependencyRegister
    {
      //  Task<LoanModel> CreateLoan(LoanModel model,  string AccountNumber, string LoginAdmin);
       // Task<LoanModel> CreateLoan(LoanModel model, string AccountNumber, string LoginAdmin, string loginId);
       // Task<bool> CreateLoan(LoanModel model, string AccountNumber, string LoginAdmin, string loginId);
        Task<SenderModel> CreateLoan(LoanModel model, string AccountNumber, string LoginAdmin, string loginId);
        IEnumerable<Loan> GetAllLoans();
        LoanModel ActivateLoan(string accountNumber);
         Task<bool> ReNewLoan(LoanModel model);
    }
}
