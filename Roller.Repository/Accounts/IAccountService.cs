using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Roller.Repository.Accounts
{
    public interface IAccountService
    {
        Task<AccountDepositModel> GetDepositAccount(AccountDepositModel request);
        Task<AccountDebitModel> GetDebitAccount(AccountDebitModel request);
        Task<AccountTransferModel> GetTransferAmount(AccountTransferModel request);
        Task<AccountDetailModel> GetAccountDetails(GetAccountDetailQuery request);
        Task<IEnumerable<CustomerAccountModel>> GetCustomerAccounts(CustomerAccountModel request);
    }
}
