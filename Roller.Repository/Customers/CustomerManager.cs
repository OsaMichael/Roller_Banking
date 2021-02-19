using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Roller.DataContext.Entity;
using Roller.DContext;
using Roller.Repository.Accounts;
using Roller.Repository.Cards;
using Roller.Repository.Enumerations;
using Roller.Repository.Extensions;
using Roller.Repository.Models;
using Roller.Repository.TransactRepo;
using Roller.Web.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Roller.Repository.Customers
{
   public class CustomerManager : ICustomerManager
    {
        private readonly RollerDataContext _context;

        private const decimal MinimumCurrentAccountBalance = 5000;
        private const int DebitLiabilityAccountOperation = -1;
        private const int CreditLiabilityAccountOperation = 1;
        //private IConfiguration _config;

        //private ILogger _logger { get; }
        private readonly IEmailSender _emailSender;
        public CustomerManager( RollerDataContext context /*ILogger<CustomerManager> logger*/)
        {
            _context = context;
            //_logger = logger;
        }

        public ShowBalanceDto GetDeposit(ShowBalanceDto model, string createdby)
        {

            //  Customer cust = _context.Customers.Where(b=>b.Id == customer.CustomerId)
            Customer cust = _context.Customers.SingleOrDefault(d => d.CustAccNumber == model.AccountNumber);

            Account acct = _context.Accounts.SingleOrDefault(d => d.AccountNumber == model.AccountNumber);

            if (cust.CustAccNumber != acct.AccountNumber)
            {
                throw new Exception("customer accont number does not match with account number");
            }


            if (cust.Cust_acc_type == "Savings")
            {
                if (model.Amount >= 1000)
                {
                    cust.Cust_balance = cust.Cust_balance + Convert.ToDecimal(model.Amount);
                    cust.Streetaddress = cust.Streetaddress;
                    cust.Surname = cust.Surname;
                    cust.Givenname = cust.Givenname;

                    //update account table when trasaction is made
                    acct.Balance = cust.Cust_balance;
                    acct.CustId = cust.Id;
                    // acct.AccountNumber = cust.CustAccNumber;

                    // Message = "";
                    // var requisitionURL = _config.GetSection("ExternalAPI:RequisitionURL").Value;

                    //var subject = "ALERT NOTIFICATION";

                    //var message = "</br><b> Dear </b>" + cust.Surname + " " + cust.Givenname;
                    //message += "</br><b> Your account: </b>" + cust.CustAccNumber;
                    //message += "</br><b> Credited with: #</b>" + model.Amount;
                    //message += "</br><b> Date of Transaction: #</b>" + cust.LastDateUpdated;
                    ////message += "</br>has been registered successful on Cyberspace E-procurement Portal.</br>";
                    ////message += "</br>Kindly, log in via " + requisitionURL + " and validate the required documents.";
                    //message += "</br>Regards";

                    //_emailSender.SendEmailAsync(cust.Emailaddress, subject, message, "");
                    Transaction tr = new Transaction();
                    tr.Amount = model.Amount;
                    tr.Tr_Date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    tr.Tr_EmpType = createdby;
                    tr.Attendant = model.Attendant;
                    tr.ReferenceNo = model.ReferenceNo;
                    tr.Depositor = model.Depositor;
                    tr.Tr_AccName = cust.Surname + " " + cust.Givenname;
                    tr.CustomerId = cust.Id;
                    tr.Type = "CashDeposit";
                    tr.AccountNumber = cust.CustAccNumber;
                    // tr.Tr_Branch = "Branch";  

                    _context.Transactions.Add(tr);
                    _context.SaveChanges();

                    //  Accounts = customer.Accounts.Select(n => new CustomerAccountDto
                    if (acct != null)
                    {
                        var you = new ShowBalanceDto();
                        you.AccountNumber = acct.AccountNumber;
                        you.Balance = acct.Balance.Value;
                        you.FullName = cust.Surname + " " + cust.Givenname;
                        you.RecipientEmail = cust.Emailaddress;
                        you.Amount = model.Amount;
                        you.AccountNumber = cust.CustAccNumber;
                        you.DateAndTransId =Convert.ToDateTime(tr.Tr_Date).ToString("yyyyMMdd") + tr.TransactionId.ToString("000");
                        you.Date = Convert.ToString(cust.LastDateUpdated);
                        //you.Depositor = message;


                        return you;

                    }

                    //fname + Now.ToString("_MMddyyyy_HHmmss")
                }
                else
                {
                    throw new Exception ("Amount has to be higher that 1000Tk/Invalid");
                }
               
            }                
               else if (cust.Cust_acc_type == "Loan")
              {

               Loan loan = _context.Loans.Where(x=>x.AccountNumber == model.AccountNumber).FirstOrDefault();

                if (model.Amount >= 1000 && loan.IsActive == true)
                {
                    loan.Loan_Amount_Paid = loan.Loan_Amount_Paid + (model.Amount);
                    loan.Total_Paid += (model.Amount);
                    //loan.Loan_Amount_Paid += Convert.ToDecimal(model.Amount);

                    Loan loantoUpdate = _context.Loans.SingleOrDefault(d => d.LoanId == loan.LoanId);
                   // Customer custToUpdate = _context.Customers.SingleOrDefault(d => d.CustAccNumber == model.AccountNumber);

                    loantoUpdate.AccountNumber = cust.CustAccNumber;
                    cust.Givenname = cust.Givenname;
                    cust.Streetaddress = cust.Streetaddress;
                    cust.Surname = cust.Surname;
                    cust.Cust_balance = loan.Loan_Amount_Paid;

                    //update loan table when trasaction is made
                    acct.Balance = loan.Loan_Amount_Paid;
                    loan.CustId = cust.Id;

                    //  return   _context.SaveChangesAsync();

                    // _context.Loans.Add(loan);

                    if (loan.Total_Paid ==  loan.AmountTo_Pay && loan.Deadline == DateTime.Now.AddDays(7).ToString("yyyy-MM-dd"))
                    {
           
                        //loan.Status = "InActive";   
                        loan.IsActive = false;
                        loantoUpdate.AccountNumber = cust.CustAccNumber;
                        cust.Givenname = cust.Givenname;
                        cust.Streetaddress = cust.Streetaddress;
                        cust.Surname = cust.Surname;
                        //loan.IsActive = false;
                    }

                    Transaction tr = new Transaction();
                    tr.Amount = model.Amount;
                    tr.Tr_Date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    tr.Tr_EmpType = createdby;
                    tr.Attendant = model.Attendant;
                    tr.ReferenceNo = model.ReferenceNo;
                    tr.Depositor = model.Depositor;
                    tr.Tr_AccName = cust.Surname + " " + cust.Givenname;
                    tr.CustomerId = cust.Id;
                    tr.Type = "Loan Deposit";
                    tr.AccountNumber = cust.CustAccNumber;
                    // tr.Tr_Branch = "Branch";               

                    _context.Transactions.Add(tr);
                    _context.SaveChanges();


                    if (acct != null)
                    {

                        var you = new ShowBalanceDto();

                        you.AccountNumber = acct.AccountNumber;
                        you.Balance = acct.Balance.Value;
                        you.FullName = cust.Surname + " " + cust.Givenname;
                        you.RecipientEmail = cust.Emailaddress;
                        you.Amount = model.Amount;
                        you.AccountNumber = cust.CustAccNumber;
                        you.DateAndTransId = Convert.ToDateTime(tr.Tr_Date).ToString("yyyyMMdd") + tr.TransactionId.ToString("000");
                        you.Date = Convert.ToString(cust.LastDateUpdated);

                        //you.AccountNumber = acct.AccountNumber;
                        //you.Balance = acct.Balance.Value;
                        //you.DateAndTransId = Convert.ToDateTime(tr.Tr_Date).ToString("yyyyMMdd") + tr.TransactionId.ToString("000");
                        return you;
                    }
                }
                else
                {
                    throw new Exception("Amount has to be higher that 1000Tk/Invalid");
                }

            }
            else
            {
                throw new Exception("Account Not Recognized");
            }

            return model;
        }
        public ShowBalanceDto GetWildrawal(ShowBalanceDto model, string createdby)
        {

            Customer custw = _context.Customers.SingleOrDefault(c => c.CustAccNumber == model.AccountNumber);
            Account acct = _context.Accounts.SingleOrDefault(d => d.AccountNumber == model.AccountNumber);

            if (custw.CustAccNumber != acct.AccountNumber)
            {
                throw new Exception("customer accont number does not match with account number");
            }

            // Customer cust = _context.Customers.SingleOrDefault(d => d.Id == model.CustomerId);

            if (custw == null)
            {
                throw new Exception("Not exists with this Id");
            }


            if (custw.Cust_balance >= Convert.ToDecimal(model.Amount + 500) && model.Amount > 0 && custw.Cust_acc_type == "Savings")
            {
                custw.Cust_balance = custw.Cust_balance - Convert.ToDecimal(model.Amount) /** DebitLiabilityAccountOperation*/;
                custw.Streetaddress = custw.Streetaddress;
                custw.Surname = custw.Surname;
                custw.Givenname = custw.Givenname;

                acct.Balance = custw.Cust_balance;
                acct.CustId = custw.Id;

                Transaction tr = new Transaction();
                tr.Amount = model.Amount;
                tr.Tr_Date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                tr.Tr_EmpType = createdby;
                tr.Attendant = model.Attendant;
                tr.ReferenceNo = model.ReferenceNo;
                tr.Depositor = model.Depositor;
                tr.Tr_AccName = custw.Surname + " " + custw.Givenname;
                tr.CustomerId = custw.Id;
                tr.Type = "CashWithdraw";
                tr.AccountNumber = custw.CustAccNumber;
                // tr.Tr_Branch = "Branch";

                _context.Transactions.Add(tr);
                _context.SaveChanges();


                if (acct != null)
                {
                    var you = new ShowBalanceDto();

                    you.AccountNumber = acct.AccountNumber;
                    you.Balance = acct.Balance.Value;
                    you.FullName = custw.Surname + " " + custw.Givenname;
                    you.RecipientEmail = custw.Emailaddress;
                    you.CustImageThumbnailUrl = custw.ImageThumbnailUrl;
                    you.Amount = model.Amount;
                    you.AccountNumber = custw.CustAccNumber;
                    you.DateAndTransId = Convert.ToDateTime(tr.Tr_Date).ToString("yyyyMMdd") + tr.TransactionId.ToString("000");
                    you.Date = Convert.ToString(custw.LastDateUpdated);

                    //you.AccountNumber = acct.AccountNumber;
                    //you.Balance = acct.Balance.Value;
                    //you.CustImageThumbnailUrl = custw.ImageThumbnailUrl;
                    //you.DateAndTransId = Convert.ToDateTime(tr.Tr_Date).ToString("yyyyMMdd") + tr.TransactionId.ToString("000");
                    return you;
                }
            }
                //else
                //{
                //    // ViewData["Message"] = "Amount has to be higher that 1000Tk/Invalid";
                //}

            
            else if (custw.Cust_acc_type == "Loan") // hangfire will do the wilthraw weekely
            {
                // LoanRepository lr = new LoanRepository();

                //  LInfo luser = lr.GetUser(ur.User_acc_no);


                //if (amount >= 1000 && luser.Status == "Active")
                //{
                //    luser.Loan_Amount_Paid += Convert.ToDouble(amount);
                //    lr.Update(luser, ur);

                //    if (luser.AmountTo_Pay == luser.Loan_Amount_Paid)
                //    {
                //        luser.Status = "InActive";
                //        lr.Update(luser, ur);
                //        ViewData["Message"] = "Loan Payment Successfull, Your Loan Account Has been Deactivated, Thank You, Sir. Come Again Soon!";
                //    }
                //}
                //else
                //{
                //    ViewData["Message"] = "Amount has to be higher that 1000Tk/Invalid";
                //}

            }
            else
            {
                //   ViewData["Message"] = "Account Not Recognized";
            }

            return model;
        }

        //public bool HasSufficientBalance(Customer account, decimal amount)
        //{
        //    if (account.AccountType == CustomerAccountType.Current)
        //    {
        //        if (account.Cust_balance < amount + MinimumCurrentAccountBalance)
        //        {
        //            return false;
        //        }
        //        else
        //        {
        //            return true;
        //        }

        //    }
        //    if (account.AccountType == CustomerAccountType.Savings)
        //    {
        //        if (account.Cust_balance >= amount)
        //        {
        //            return true;
        //        }
        //        return false;
        //    }
        //    return false;
        //}

        //Implement GetViaAccountNumber
        public Customer GetAccountViaAccountNumber(string accountNumber)
        {
            var customerAccount = _context.Customers.Where(p => p.IsActive == true).SingleOrDefault(p => p.CustAccNumber == accountNumber);
            return customerAccount;
        }
        public  Customer GetAccountVaiAccountNumber(string accountNumber)
        {
            var customerAccount = _context.Customers.Where(p => p.CustAccNumber == accountNumber).FirstOrDefault();
            if (customerAccount != null) throw new Exception("already exist");
            return customerAccount;
        }
        public int Update(Customer customer)
        {
            Customer custToUpdate = _context.Customers.SingleOrDefault(d => d.Id == customer.Id);
            custToUpdate.Givenname = customer.Givenname;
            custToUpdate.Surname = customer.Surname;


           _context.Customers.Add(custToUpdate);
            return _context.SaveChanges();
           // return result.Entity;

                // return result;
        }

        public bool Showbanlace(string accountNumber)
        {
            var balance = _context.Customers.Where(v => v.CustAccNumber == accountNumber);

            var result = new CustomerDto
            {
                   Amount = Convert.ToDecimal(balance.Select(c=>c.Cust_balance)),
                   AccountNumber = accountNumber
            };
            return true;
        }
        public  async Task<bool> CreateCustomer(CustomerProfile request, string accountNumber, string loginAdmin)
        {

            var customer = new Customer
            {
                CustAccNumber = accountNumber,
                Cust_acc_type = request.AccountType,
                BVNNumber = request.BVNNumber,
                ImageThumbnailUrl = request.CustImageThumbnailUrl,
                UploadDocument = request.ScannThumbnailUrl,
                ImageUrl = request.ImageUrl,
                Givenname = request.GivenName.ToFirstLetterUpper(),
                Surname = request.Surname.ToFirstLetterUpper(),
                Gender = request.Gender,
               
                CreatedBy = loginAdmin,
                OtherNames = request.OtherNames,
                City = request.Address.City.ToUpperInvariant(),
                Country = request.Address.Country.ToFirstLetterUpper(),
                Streetaddress = request.Address.StreetAdress.ToFirstLetterUpper(),
                Emailaddress = request.EmailAdress.ToFirstLetterUpper(),
               // CountryCode = request.Address.CountryCode.ToUpperInvariant(),
                //Zipcode = request.Address.ZipCode,
                Birthday = request.Birthday,
                //NationalId = request.NationalId,
                //Telephonecountrycode = request.TelephoneCountryCode,
                Telephonenumber = request.TelephoneNumber,


 
        };
            var account = new Account
            {
                Balance = 0m,
                Created = DateTime.Now,
                Frequency = AccountFrequency.Monthly,
                AccountNumber = accountNumber,
                CustId = customer.Id

            };
            _context.Customers.Add(customer);

            _context.Accounts.Add(account);
            //_context.SaveChanges();
            var result = await _context.SaveChangesAsync();


            //var disposition = new Disposition
            //{
            //    CustomerId = customer.Id,
            //    AccountId = account.Id,
            //    Type = DispositionType.Owner
            //};

            //_context.Dispositions.Add(disposition);

            //  var result = await _context.SaveChangesAsync();

            //if (result == 1)
            //{

            //   // throw new Exception("Successfully registred customer");

            //    //return new BaseResult { IsSuccess = true, Success = "Successfully registred customer" };
            //}
            //else
            //{
            //    // throw new Exception("Successfully registred customer");
            //   //  throw new Exception("An error occured while registrering the customer, try again shortly.");
            //}
            // throw new Exception("An error occured while registrering the customer, try again shortly.");
            // return new BaseResult { IsSuccess = false, Success = "An error occured while registrering the customer, try again shortly." };

            return true;
        }


        public async Task<bool> CreateUser(CustomerProfile request, string accountNumber, string userEmail)
        {

            Customer customer = new Customer
            {
                CustAccNumber = accountNumber,
                Cust_acc_type = request.AccountType,
                BVNNumber = request.BVNNumber,
                Givenname = request.GivenName.ToFirstLetterUpper(),
                Surname = request.Surname.ToFirstLetterUpper(),
                Gender = request.Gender,
                CreatedBy = userEmail,
                OtherNames = request.OtherNames,
                City = request.Address.City.ToUpperInvariant(),
                Country = request.Address.Country.ToFirstLetterUpper(),
                Streetaddress = request.Address.StreetAdress.ToFirstLetterUpper(),
                Emailaddress = request.EmailAdress.ToFirstLetterUpper(),
                Birthday = request.Birthday,
                Telephonenumber = request.TelephoneNumber,

            };

            var account = new Account
            {
                Balance = 0m,
                Created = DateTime.Now,
                AccountNumber = accountNumber,
                CustId = customer.Id

            };
            _context.Customers.Add(customer);

            _context.Accounts.Add(account);
            //_context.SaveChanges();
            var result = await _context.SaveChangesAsync();


            //var disposition = new Disposition
            //{
            //    CustomerId = customer.Id,
            //    AccountId = account.Id,
            //    Type = DispositionType.Owner
            //};

            //_context.Dispositions.Add(disposition);

            //  var result = await _context.SaveChangesAsync();

            //if (result == 1)
            //{

            //   // throw new Exception("Successfully registred customer");

            //    //return new BaseResult { IsSuccess = true, Success = "Successfully registred customer" };
            //}
            //else
            //{
            //    // throw new Exception("Successfully registred customer");
            //   //  throw new Exception("An error occured while registrering the customer, try again shortly.");
            //}
            // throw new Exception("An error occured while registrering the customer, try again shortly.");
            // return new BaseResult { IsSuccess = false, Success = "An error occured while registrering the customer, try again shortly." };

            return true;
        }



        public async Task<Customer> updateCustomer(CustomerProfile request)
        {
            var customer = await _context.Customers.SingleOrDefaultAsync(c => c.Id == request.CustomerId);

            if (customer == null)
            {
                throw new NotFoundException(nameof(Customer), request.CustomerId);
            }

            customer.City = request.Address.City.ToUpperInvariant();
            customer.Country = request.Address.Country.ToFirstLetterUpper();
            customer.Streetaddress = request.Address.StreetAdress.ToFirstLetterUpper();
           // customer.Zipcode = request.Address.ZipCode;
           // customer.CountryCode = request.Address.CountryCode.ToUpperInvariant();
            customer.Gender = request.Gender;
            customer.Birthday = request.Birthday;
            customer.Surname = request.Surname.ToFirstLetterUpper();
            customer.Givenname = request.GivenName.ToFirstLetterUpper();
            customer.Emailaddress = request.EmailAdress.ToFirstLetterUpper();
            customer.Telephonenumber = request.TelephoneNumber;
            //customer.Telephonecountrycode = request.TelephoneCountryCode;
            //customer.NationalId = request.NationalId;

            _context.Customers.Update(customer);
            await _context.SaveChangesAsync();

         
             //   throw new Exception("Successfully updated the customers profile.");
            //  return new BaseResult { IsSuccess = true, Success = "Successfully updated the customers profile." };
            return customer;
        }
        public async Task<CustomerProfile> GetCustomIDandEmail(GetCustomerQuery request)
        {
            Customer customer;
            if (!string.IsNullOrEmpty(request.CustomerEmail))
            {
                customer = await _context.Customers.SingleOrDefaultAsync(c => c.Emailaddress == request.CustomerEmail);
            }
            else
            {
                customer = await _context.Customers.SingleOrDefaultAsync(c => c.Id == request.CustomerId);
            }
            if (customer == null)
            {
                throw new NotFoundException(nameof(Customer), request.CustomerId);
            }

            var model = new CustomerProfile
            {
                CustomerId = customer.Id,
                TelephoneNumber = customer.Telephonenumber,
                //TelephoneCountryCode = customer.Telephonecountrycode,
                //NationalId = customer.NationalId,
                Surname = customer.Surname,
                EmailAdress = customer.Emailaddress,
                GivenName = customer.Givenname,
                Birthday = customer.Birthday,
                Gender = customer.Gender,
                Address = new CustomerAddress
                {
                    //CountryCode = customer.CountryCode,
                    Country = customer.Country,
                    City = customer.City,
                    StreetAdress = customer.Streetaddress,
                   // ZipCode = customer.Zipcode
                }
            };

            return model;
        }

        //public async Task<CustomerDto> GetCustomerDetails(GetCustomerDetailsQuery request)
        public async Task<CustomerDto> GetCustomerDetails(string accountNumber)
        {
            var cust = _context.Customers.Where(b => b.CustAccNumber == accountNumber);
            //var account = _context.Accounts.Where(b => b.AccountNumber == accountNumber);
            if (cust == null) throw new Exception("user not found");

            GetCustomerDetailsQuery request = new GetCustomerDetailsQuery();

            request.AccountNumber = accountNumber;
            

            Customer customer;
            if (!string.IsNullOrEmpty(request.CustomerEmail))
            {
                customer = await _context.Customers
                    .SingleOrDefaultAsync(c => string.Equals(c.Emailaddress, request.CustomerEmail, StringComparison.CurrentCultureIgnoreCase));
            }
            else
            {
                customer = await _context.Customers.SingleOrDefaultAsync(c => c.CustAccNumber == request.AccountNumber);
            }
            if (customer == null)
            {
                throw new NotFoundException(nameof(Customer), request.AccountNumber);
            }
            var acct = new GetCustomerAccountsQuery();
          

            var model = new CustomerDto
            {
                GivenName = customer.Givenname,
                Surname = customer.Surname,
                CustomerId = customer.Id,
                AccountNumber = customer.CustAccNumber,
                Birthday = customer.Birthday,
                EmailAdress = customer.Emailaddress,
                TelephoneCountryCode = customer.Telephonecountrycode,
                TelephoneNumber = customer.Telephonenumber,
                NationalId = customer.NationalId,
                Gender = customer.Gender,
                 CustImageThumbnailUrl = customer.ImageThumbnailUrl,
                  ScannThumbnailUrl = customer.UploadDocument,
               // ImageUrl = customer.ImageUrl,
                Address = new CustomerAddress
                {
                    Country = customer.Country,
                    City = customer.City,
                 //   CountryCode = customer.CountryCode,
                    StreetAdress = customer.Streetaddress,
                   // ZipCode = customer.Zipcode
                },
                // Accounts =   customer.a
                Accounts = customer.Accounts.Select(n => new CustomerAccountDto
                {
                    CustomerId = n.CustId,
                    AccountNumber = n.AccountNumber,
                    Balance = n.Balance.Value

                }).ToList(),
                ////Accounts = new List<CustomerAccountDto>(customer.Id).ToList(),


                //////new List<CustomerAccountDto>(customer.CustomerId),
                //Cards = customer.Cards.Select(n => new CardDto
                //{
                //    CustomerId = n.CardId
                //})

            };
            return model;
        }

        public async Task<CustomerListViewModel> GetCustomerList(GetCustomerListQuery request)
        {
            var query = _context.Customers.AsQueryable().AsNoTracking();

            var nameExists = !string.IsNullOrWhiteSpace(request.Name);
            var accountNoExist = !string.IsNullOrWhiteSpace(request.AccountNumber);

            if (nameExists && !accountNoExist)
            {
                var names = request.Name.Split(" ");
                var firstName = names[0];
                if (names.Length > 1)
                {
                    var surName = names[1];
                    query = query
                        .Where(c => c.Givenname.StartsWith(firstName, StringComparison.OrdinalIgnoreCase) && c.Surname.StartsWith(surName))
                        .OrderByDescending(c => c.Givenname.Equals(firstName, StringComparison.OrdinalIgnoreCase))
                        .ThenByDescending(c => c.Surname.Equals(surName, StringComparison.OrdinalIgnoreCase))
                        .AsNoTracking();
                }
                else
                {
                    query = query
                        .Where(c => c.Givenname.StartsWith(firstName, StringComparison.OrdinalIgnoreCase) ||
                                    c.Surname.StartsWith(firstName, StringComparison.OrdinalIgnoreCase))
                        .OrderByDescending(c => c.Givenname.Equals(firstName, StringComparison.OrdinalIgnoreCase))
                        .ThenByDescending(c => c.Givenname.StartsWith(firstName, StringComparison.OrdinalIgnoreCase))
                        .ThenByDescending(c => c.Surname.StartsWith(firstName)).AsNoTracking();
                }
            }
            else if (accountNoExist && !nameExists)
            {
                query = query.Where(c => c.CustAccNumber
                        .StartsWith(request.AccountNumber, StringComparison.OrdinalIgnoreCase))
                        .OrderByDescending(c => c.CustAccNumber.Equals(request.AccountNumber, StringComparison.OrdinalIgnoreCase))
                    .ThenByDescending(c => c.CustAccNumber.StartsWith(request.AccountNumber, StringComparison.OrdinalIgnoreCase))
                    .AsNoTracking();

            }
            else if (accountNoExist && nameExists)
            {
                var names = request.Name.Split(" ");
                var firstName = names[0];
                if (names.Length > 1)
                {
                    var surName = names[1];
                    query = query.Where(c =>
                            (c.Givenname.StartsWith(firstName, StringComparison.OrdinalIgnoreCase) &&
                             c.Surname.StartsWith(surName, StringComparison.OrdinalIgnoreCase)) &&
                             c.CustAccNumber.StartsWith(request.AccountNumber, StringComparison.OrdinalIgnoreCase))
                        .OrderByDescending(c => c.Givenname.StartsWith(request.Name, StringComparison.OrdinalIgnoreCase))
                        .ThenByDescending(c => c.Surname.StartsWith(request.Name, StringComparison.OrdinalIgnoreCase))
                        .ThenByDescending(c => c.CustAccNumber.StartsWith(request.AccountNumber, StringComparison.OrdinalIgnoreCase)).AsNoTracking();

                }
                else
                {
                    query = query.Where(c =>
                            (c.Givenname.StartsWith(request.Name, StringComparison.OrdinalIgnoreCase) ||
                             c.Surname.StartsWith(request.Name, StringComparison.OrdinalIgnoreCase)) &&
                             c.CustAccNumber.StartsWith(request.AccountNumber, StringComparison.OrdinalIgnoreCase))
                        .OrderByDescending(c => c.Givenname.StartsWith(request.Name, StringComparison.OrdinalIgnoreCase))
                        .ThenByDescending(c => c.Surname.StartsWith(request.Name, StringComparison.OrdinalIgnoreCase))
                        .ThenByDescending(c => c.CustAccNumber.StartsWith(request.AccountNumber, StringComparison.OrdinalIgnoreCase)).AsNoTracking();
                }
            }
            else
            {
                query = query.OrderBy(c => c.Id).AsNoTracking();
            }

           // var trans = _context.Transactions.Where(d => d.AccountNumber == request.AccountNumber).ToListAsync();
            Account acct = _context.Accounts.SingleOrDefault(d => d.AccountNumber == request.AccountNumber);
            var totalCount = query.Count();
            return new CustomerListViewModel
            {
                Customers = await query.Skip(request.Offset).Take(request.Limit).Select(c => new CustomerListDto
                {
                    CustomerId = c.Id,
                    Surname = c.Surname,
                    GivenName = c.Givenname,
                    NationalId = c.NationalId,
                    AccountNumber = c.CustAccNumber,
                    Birthdate = c.Birthday.Value,
                      ScannThumbnailUrl = c.UploadDocument,
                     CustImageThumbnailUrl = c.ImageThumbnailUrl,
                     Balance = acct.Balance.Value,
                     FullName = c.Surname + "" + c.Givenname + " - " + c.CustAccNumber,
                      
                    Address = new CustomerAddress
                    {
                        City = c.City,
                        StreetAdress = c.Streetaddress,
                        //ZipCode = c.Zipcode,
                        Country = c.Country
                    }
                }).AsNoTracking().ToArrayAsync(),
                Total = totalCount,
                NumberOfPages = totalCount / request.Limit + 1,
                HasMorePages = request.Offset + request.Limit < totalCount,
                Name = request.Name,
                AccountNumber = request.AccountNumber,
                CurrentPage = request.CurrentPage,
                HasPreviousPages = request.Offset > 0
            };
        }

        public CustomerListViewModel GetCustomerAndLoanList(GetCustomerListQuery request)
        {
            var query =  (from l in  _context.Loans.SingleOrDefault(x=>x.AccountNumber ==  request.AccountNumber).AccountNumber
                        join c in _context.Customers
                        on l equals c.Id
                        join a in _context.Accounts
                        on l equals a.AccountId
                        
                        group l by new
                        {
                            l,
                            c.Givenname,
                            c.Id,
                            c.Surname,
                            c.NationalId,
                            c.CustAccNumber,
                            c.Birthday,
                            c.UploadDocument,
                            c.ImageThumbnailUrl,
                            a.Balance,
                            c.City,
                            c.Country,
                            c.Streetaddress
                        } into g

                        select new
                        {
                            Surname = g.Key.Surname,
                            LoanId = g.Key.l,
                            Givenname = g.Key.Givenname,
                            Id = g.Key.Id,
                            NationalId = g.Key.NationalId,
                            CustAccNumber = g.Key.CustAccNumber,
                            Birthday  = g.Key.Birthday,
                            UploadDocument = g.Key.UploadDocument,
                            ImageThumbnailUrl = g.Key.ImageThumbnailUrl,
                            Balance = g.Key.Balance,
                            City = g.Key.City,
                            Country = g.Key.Country,
                            Streetaddress = g.Key.Streetaddress
                        }).ToList();


           
            //// var trans = _context.Transactions.Where(d => d.AccountNumber == request.AccountNumber).ToListAsync();
            Account acct =  _context.Accounts.SingleOrDefault(d => d.AccountNumber == request.AccountNumber);
          
            return new CustomerListViewModel
            {
            Customers = query.Select(c => new CustomerListDto
            {
                CustomerId = c.Id,
                Surname = c.Surname,
                GivenName = c.Givenname,
                NationalId = c.NationalId,
                AccountNumber = c.CustAccNumber,
                Birthdate = c.Birthday.Value,
                ScannThumbnailUrl = c.UploadDocument,
                CustImageThumbnailUrl = c.ImageThumbnailUrl,
                Balance = acct.Balance.Value,
                LoanId = c.LoanId,
                Address = new CustomerAddress
                {
                    City = c.City,
                    StreetAdress = c.Streetaddress,
                    //ZipCode = c.Zipcode,
                    Country = c.Country
                }
            })
                //Total = totalCount,
                //NumberOfPages = totalCount / request.Limit + 1,
                //HasMorePages = request.Offset + request.Limit < totalCount,
                //Name = request.Name,
                //AccountNumber = request.AccountNumber,
                //CurrentPage = request.CurrentPage,
                //HasPreviousPages = request.Offset > 0
            };
           // return result;
        }

        public async Task<bool> UpdateProfile(EditProfileDto profile, string LoginUser)
        {

            var user = _context.Customers.SingleOrDefault(x =>x.Id == profile.CustomerId);
            //Customer customer = new Customer
            //{
                 
            //};

            user.Givenname = profile.GivenName;
            user.ImageThumbnailUrl = profile.CustImageThumbnailUrl;
            user.OtherNames = profile.OtherNames;
            user.Surname = profile.Surname;
            user.Streetaddress = profile.StreetAddress.ToString();
            user.UpdatedBy = LoginUser;
            user.Emailaddress = profile.EmailAdress;
            user.LastDateUpdated = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            await _context.SaveChangesAsync();
            return true; 
        }
        public Customer GetCustomID(int id)
        {
            var custID = _context.Customers.SingleOrDefault(x=>x.Id == id);

            //var Detals = n
            //{
            //    GetCustomerDetails
            //  //   Accounts  = custID
            //};
            return custID;
        }
        public Customer GetCustomEmailwithUserSignin(string email)
        {
            var custID = _context.Customers.SingleOrDefault(x => x.Emailaddress == email);

            //var Detals = n
            //{
            //    GetCustomerDetails
            //  //   Accounts  = custID
            //};
            return custID;
        }
        public IEnumerable<Transaction> GetTransactions(string accountNumber)
        {
            return _context.Transactions.Where(u => u.AccountNumber == accountNumber).ToList();

            //var result = new TransViewModel();
          
            //result.AccountNumber = trans.
        }
       
    }
    
}
