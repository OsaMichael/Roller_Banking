using Roller.DataContext.Entity;
using Roller.DContext;
using Roller.Repository.Enumerations;
using Roller.Repository.Extensions;
using Roller.Web.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roller.Repository.LoanRepos
{
   public class LoanRepository: ILoanRepository
    {
        private readonly RollerDataContext _context;
        public LoanRepository(RollerDataContext context)
        {
            _context = context;
        }

        public async Task<SenderModel> CreateLoan (LoanModel model, string AccountNumber, string LoginAdmin,string loginId)
        {
            //  Loan loan = _context.Loans.Where(x=>x)

            var checkExist2 = _context.Customers.FirstOrDefault(x => x.CustAccNumber == model.AccountNumber
       && x.Telephonenumber == model.Phone && x.Emailaddress == model.EmailAdress);
            if (checkExist2 != null) throw new Exception("account number, phone number or email already exist");

            var checkExist = _context.Loans.FirstOrDefault(x => x.AccountNumber == model.AccountNumber);
            if (checkExist != null) throw new Exception("account number already exist");     

            Customer customer = new Customer
            {
             Streetaddress = model.Address.ToFirstLetterUpper(),
            Cust_balance = 0,
            Telephonenumber = model.Phone,
            Surname = model.Surname.ToFirstLetterUpper(),
            Givenname = model.Givenname.ToFirstLetterUpper(),
            OtherNames = model.OtherNames.ToFirstLetterUpper(),
            Deadline = DateTime.Now.AddMonths(model.Duration).ToString("yyyy-MM-dd"),
            //Deadline = DateTime.Now.AddMonths(model.Duration).ToString("MM"),
            CustAccNumber = AccountNumber,
            BVNNumber = model.BVNNumber,
            ImageThumbnailUrl = model.CustImageThumbnailUrl,
            UploadDocument = model.ScannThumbnailUrl,
            Birthday = model.Birthday,
            Cust_acc_type = "Loan",
            CreatedBy = LoginAdmin,
            Country = model.Country.ToFirstLetterUpper(),
            City = model.City.ToFirstLetterUpper(),
            Emailaddress = model.EmailAdress.ToFirstLetterUpper(),
            Gender = model.Gender,
        };

           // customer.Cust_acc_type = model.AccountType.ToFirstLetterUpper();
          
            _context.Customers.Add(customer);


            var account = new Account
            {
                Balance = 0m,
                Created = DateTime.Now,
                Frequency = AccountFrequency.Weekly,
                AccountNumber = AccountNumber,
                CustId = customer.Id
            };
            _context.Accounts.Add(account);

            //if(model.Frequency == "Weekly")
            //{
                Loan loan = new Loan();

                loan.LoanCause = model.LoanCause;
                loan.Loan_Amount = model.Loan_Amount;
                loan.Deadline = DateTime.Now.AddMonths(model.Duration).ToString("yyyy-MM-dd");
                loan.Duration = model.Duration;
                loan.Interest_Rate = model.Interest_Rate;
               // loan.Deadline = DateTime.Now.AddYears(Loan_Deadline).ToString("yyyy-MM-dd");
                loan.Frequency = model.Frequency;

                loan.AmountTo_Pay = loan.Loan_Amount + ((loan.Loan_Amount * loan.Interest_Rate) / 100);
                
                loan.IsActive = true;
                loan.Loan_Amount_Paid = 0;
                loan.Loan_Date = DateTime.Now.ToString("yyyy-MM-dd");
                loan.Manager_Approval = "No";
                loan.LOfficer_Id = Convert.ToInt32(loginId);
                loan.AccountNumber = AccountNumber;
                loan.AccountId = account.AccountId;
                loan.CustId = customer.Id;

                _context.Loans.Add(loan);


            //}
            //else if (model.Frequency == "Monthly")
            //{

            //}
            //  throw new Exception("Loan Account Successfully Created");

            Transaction tr = new Transaction();
            tr.Amount = Convert.ToDecimal( model.Loan_Amount);
            tr.Tr_Date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            tr.Tr_EmpType = "LOfficer";
            tr.Tr_AccName = customer.Surname + " " + customer.Givenname + " " + customer.OtherNames;
            tr.Type = "Loan";
            tr.AccountNumber = AccountNumber;
           // tr.AccountNumber = 

            _context.Transactions.Add(tr);

          await  _context.SaveChangesAsync();

            var notify = new SenderModel();

            notify.Amount = loan.AmountTo_Pay;
            notify.Date = loan.Loan_Date;
            notify.AmountGiven = loan.Loan_Amount;
            notify.recepientName = customer.Surname + " " + customer.Givenname + " " + customer.OtherNames;
            notify.recipientEmail = customer.Emailaddress;
            notify.accountNumber = loan.AccountNumber;
            notify.Deadline = loan.Deadline;
            notify.Interest_Rate = loan.Interest_Rate;
            notify.Frequency = loan.Frequency;
            return notify;
        }

        public async Task<bool> ReNewLoan(LoanModel model)
        {
            //  Loan loan = _context.Loans.Where(x=>x)
            try {

                Customer customer = _context.Customers.SingleOrDefault(x => x.CustAccNumber == model.AccountNumber);
                Account account = _context.Accounts.SingleOrDefault(x => x.AccountNumber == model.AccountNumber);

            Loan loan = _context.Loans.SingleOrDefault(x => x.LoanId == model.LoanId);

            //loan.LoanCause = model.LoanCause;
            loan.Loan_Amount =  model.Loan_Amount;
            loan.Deadline =   DateTime.Now.AddMonths(model.Duration).ToString("yyyy-MM-dd");
            loan.Duration = model.Duration;
            loan.Interest_Rate =  model.Interest_Rate;
            // loan.Deadline = DateTime.Now.AddYears(Loan_Deadline).ToString("yyyy-MM-dd");
            loan.Frequency =  model.Frequency;

            loan.AmountTo_Pay = loan.Loan_Amount + ((loan.Loan_Amount * loan.Interest_Rate) / 100);

            loan.IsActive = true;
            loan.Loan_Amount_Paid = 0;
            loan.Total_Paid = 0;
            loan.Loan_Date =  DateTime.Now.ToString("yyyy-MM-dd");

            customer.Cust_balance = loan.Loan_Amount_Paid;
            //customer.DateCreated = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                account.Balance = loan.Loan_Amount_Paid;
                account.Created = DateTime.Now;
                //_context.Loans.Add(loan);
                await _context.SaveChangesAsync();

                //Transaction tr = new Transaction();
                //tr.Amount = Convert.ToDecimal(model.Loan_Amount);
                //tr.Tr_Date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                //tr.Tr_EmpType = "LOfficer";
                //tr.Tr_AccName = customer.Surname + " " + customer.Givenname + " " + customer.OtherNames;
                //tr.Type = "Loan";
                //tr.AccountNumber = model.AccountNumber;
                //// tr.AccountNumber = 

                //_context.Transactions.Add(tr);

                //await _context.SaveChangesAsync();
            }
            catch (Exception xx)
            {
                throw xx;
            }
            return true;
         
        }
        public LoanModel ActivateLoan(string accountNumber)
        {
           // var accountNo = _context.Customers.Where(x=>x.CustAccNumber == accountNumber).FirstOrDefault();
            var loanAccount = _context.Loans.Where(c => c.AccountNumber == accountNumber).FirstOrDefault();
            //var loanAccount = _context.Loans.Where(c =>c.AccountNumber == accountNumber).FirstOrDefault();
           // if (accountNo == null) throw new Exception("user not found");
            var result = new LoanModel 
            {
                 AccountNumber = loanAccount.AccountNumber,
                 Deadline = loanAccount.Deadline,
                 Loan_Amount = loanAccount.Loan_Amount,
                 Interest_Rate = loanAccount.Interest_Rate,
                 AmountTo_Pay = loanAccount.AmountTo_Pay,
                 Loan_Date = loanAccount.Loan_Date,
                 Duration = loanAccount.Duration,
                  Frequency = loanAccount.Frequency,
                  LoanId = loanAccount.LoanId
            };

            return result;

        }
        public IEnumerable<Loan> GetAllLoans()
        {
            var loan = _context.Loans.Where(x=>x.IsActive == true).ToList();

            return loan;
            //var ln = new Loan();
            //ln.Interest_Rate = loan.
            //var result = new TransViewModel();

            //result.AccountNumber = trans.
        }
    }
}

