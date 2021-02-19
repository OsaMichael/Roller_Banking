using Microsoft.EntityFrameworkCore;
using Roller.DataContext.Entity;
using Roller.DContext;
using Roller.Repository.Enumerations;
using Roller.Repository.Extensions;
using Roller.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roller.Repository.Accounts
{
  public  class AccountService
    {
        private readonly RollerDataContext _context;
        //private readonly IDateTime _time;
        public AccountService(RollerDataContext contex)
        {
            _context = contex;
        }
        public async Task<AccountDepositModel> GetDepositAccount(AccountDepositModel request)
        {
            if (request.Amount < 0)
            {
                throw new NotFoundException(nameof(Account), request.AccountId);
            }

            var account = await _context.Accounts.SingleOrDefaultAsync(a => a.AccountId == request.AccountId);
            if (account == null)
            {
                throw new NotFoundException(nameof(Account), request.AccountId);
            }

            request.Amount = Math.Round(request.Amount, 2);
            account.Balance += request.Amount;

            var transaction = new Transaction
            {
                CustomerId = account.AccountId,
                Balance = Math.Round(account.Balance ?? 0, 2),
                Date = DateTime.Now,
                Type = TransactionType.Credit,
                Amount = request.Amount,
                Operation = Operation.Credit
            };

            _context.Accounts.Update(account);
            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();

            var message = new BaseResult
            {
                IsSuccess = true,
                Success = $"Successfully deposited {request.Amount.ToSwedishKrona()} to account #{request.AccountId}"
            };

            return request;
            //return new BaseResult
            //{
            //    IsSuccess = true,
            //    Success = $"Successfully deposited {request.Amount.ToSwedishKrona()} to account #{request.AccountId}"
            //};

        }
        public async Task<AccountDebitModel> GetDebitAccount(AccountDebitModel request)
        {
            if (request.Amount < 0)
            {
                throw new NegativeAmountException(nameof(Account), request.AccountId);
            }

            var account = await _context.Accounts.SingleOrDefaultAsync(a => a.AccountId == request.AccountId);
            if (account == null)
            {
                throw new NotFoundException(nameof(Account), request.AccountId);
            }
            if (account.Balance != null && account.Balance.Value - request.Amount < 0)
            {
                throw new InsufficientFundsException(nameof(Account), request.AccountId);
            }
            request.Amount = Math.Round(request.Amount, 2);

            account.Balance = account.Balance - request.Amount;
            //account.Balance -= request.Amount;
            var transaction = new Transaction
            {
                CustomerId = account.AccountId,
                Balance = Math.Round(account.Balance.Value, 2),
                Date = DateTime.Now,
                Amount = -request.Amount,
                Type = TransactionType.Debit,
                Operation = Operation.Withdrawal,
            };
            _context.Accounts.Update(account);
            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();

            var message = new BaseResult
            {
                IsSuccess = true,
                Success = $"Successfully deposited {request.Amount.ToSwedishKrona()} to account #{request.AccountId}"
            };

            return request;
            //return new BaseResult
            //{
            //    IsSuccess = true,
            //    Success = $"Successfully debited {request.Amount.ToSwedishKrona()} from account #{request.AccountId}."
            //};
        }
        public async Task<AccountTransferModel> GetTransferAmount(AccountTransferModel request)
        {
            if (request.Amount < 0)
            {
                throw new NegativeAmountException(nameof(Account), request.AccountIdFrom);
            }

            var accountFrom = await _context.Accounts.SingleOrDefaultAsync(a => a.AccountId == request.AccountIdFrom);
            var accountTo = await _context.Accounts.SingleOrDefaultAsync(a => a.AccountId == request.AccountIdTo);
                    

            if (accountFrom == null)
            {
                throw new NotFoundException(nameof(Account), request.AccountIdFrom);
            }
            if (accountTo == null)
            {
                throw new NotFoundException(nameof(Account), request.AccountIdTo);
            }
            if (accountFrom.Balance != null && accountFrom.Balance.Value - request.Amount < 0)
            {
                throw new InsufficientFundsException(nameof(Account), request.AccountIdFrom);
            }
            request.Amount = Math.Round(request.Amount, 2);

            accountFrom.Balance -= request.Amount;
            accountTo.Balance += request.Amount;
            _context.Accounts.UpdateRange(accountFrom, accountTo);

            var transactionFrom = new Transaction
            {
                CustomerId = request.AccountIdFrom,
                Balance = Math.Round(accountFrom.Balance.Value, 2),
                Amount = -request.Amount,
                Date = DateTime.Now,
                Type = TransactionType.Debit,
                Operation = Operation.TransferDebit
            };
            var transactionTo = new Transaction
            {
                CustomerId = request.AccountIdTo,
                Balance = Math.Round(accountTo.Balance.Value, 2),
                Amount = request.Amount,
                Operation = Operation.Transfer,
                Type = TransactionType.Credit,
                Date = DateTime.Now,
            };
            await _context.Transactions.AddRangeAsync(transactionFrom, transactionTo);

            await _context.SaveChangesAsync();

            var message = new BaseResult
            {
                IsSuccess = true,
                Success = $"Successfully transfered {request.Amount.ToSwedishKrona()} from #{request.AccountIdFrom} to #{request.AccountIdTo}."
            };

            return request;
            //return new BaseResult
            //{
            //    IsSuccess = true,
            //    Success = $"Successfully transfered {request.Amount.ToSwedishKrona()} from #{request.AccountIdFrom} to #{request.AccountIdTo}."
            //};
        }
        public async Task<AccountDetailModel> GetAccountDetails(GetAccountDetailQuery request)
        {
            var account = await _context.Accounts.Include(a => a.Loans).Include(a => a.PermenentOrders)
                .SingleOrDefaultAsync(a => a.AccountId == request.AccountId);
            if (account == null)
            {
                throw new NotFoundException(nameof(Account), request.AccountId);
            }

            return new AccountDetailModel
            {
                AccountId = account.AccountId,
                Balance = account.Balance,
                //Dispositions = account.Dispositions.Select(d => new DispositionDto
                //{
                //    Type = d.Type,
                //    CustomerId = d.CustomerId
                //}).ToList(),
                Loans = account.Loans.Select(l => new LoanDto
                {
                    Status = l.Status,
                    Amount = Convert.ToDecimal( l.Amount),
                    Duration = l.Duration,
                    Date = l.Date,
                    Payments = Convert.ToDecimal(l.Payments),
                    LoanId = l.LoanId
                }).ToList(),
                PermanentOrders = account.PermenentOrders.Select(p => new PermanentOrderDto
                {
                    Id = p.OrderId,
                    AccountTo = p.AccountTo,
                    Amount = p.Amount,
                    Bank = p.BankTo,
                    Symbol = p.Symbol
                }).ToList()
            };
        }
        //public async Task<IEnumerable<CustomerAccountModel>> GetCustomerAccounts(EditUserModel request)
        //{

        //}
    }
}
