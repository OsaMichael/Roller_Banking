
using Roller.DataContext.Entity;
using Roller.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Roller.Repository.Customers
{
   public interface ICustomerManager: IDependencyRegister
    {
        Customer GetCustomEmailwithUserSignin(string email);
        Task<bool> UpdateProfile(EditProfileDto profile, string LoginUser);
       // Task<bool> UpdateProfile(CustomerProfile profile, string LoginUser);
        Task<CustomerProfile> GetCustomIDandEmail(GetCustomerQuery request);
      //  Task<CustomerDto> GetCustomerDetails(GetCustomerDetailsQuery request);
        Task<CustomerListViewModel> GetCustomerList(GetCustomerListQuery request);
        Task<Customer> updateCustomer(CustomerProfile request);
      //  Task<Customer> CreateCustomer(CustomerProfile request);
        Customer GetCustomID(int id);
        //Task<CustomerDto> GetCustomerDetails(int CustomerId);
        Task<CustomerDto> GetCustomerDetails(string accountNumber);
        int Update(Customer customer);
        //bool GetDeposit(CustomerDto model, string createdby);
        //Customer Update(Customer customer);
        Customer GetAccountViaAccountNumber(string accountNumber);
        Customer GetAccountVaiAccountNumber(string accountNumber);
        // Task<Customer> CreateCustomer(CustomerProfile request, string accountNumber, string loginAdmin);
        //  bool CreateCustomer(CustomerProfile request, string accountNumber, string loginAdmin);
        Task<bool> CreateCustomer(CustomerProfile request, string accountNumber, string loginAdmin);
        // bool GetWildrawal(CustomerDto model, string createdby);
        ShowBalanceDto GetWildrawal(ShowBalanceDto model, string createdby);
        bool Showbanlace(string accountNumber);
      //  CustomerDto GetDeposit(CustomerDto model, string createdby);
        ShowBalanceDto GetDeposit(ShowBalanceDto model, string createdby);
        IEnumerable<Transaction> GetTransactions(string accountNumber);
        CustomerListViewModel GetCustomerAndLoanList(GetCustomerListQuery request);
        Task<bool> CreateUser(CustomerProfile request, string accountNumber, string userEmail);
    }
}
