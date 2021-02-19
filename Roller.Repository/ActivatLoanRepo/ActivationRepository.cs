//using Roller.DataContext.Entity;
//using Roller.DContext;
//using Roller.Repository.Enumerations;
//using Roller.Repository.Extensions;
//using Roller.Repository.LoanRepos;
//using System;
//using System.Collections.Generic;
//using System.Text;
//using System.Threading.Tasks;

//namespace Roller.Repository.ActivatLoanRepo
//{
//  public  class ActivationRepository: IActivationRepository
//    {
//        private readonly RollerDataContext _context;

//        public ActivationRepository(RollerDataContext context)
//        {
//            _context = context;
//        }
//        public async Task<bool> ActivateLoan(ActivateLoanModel model, string AccountNumber, string LoginAdmin, string loginId)
//        {
//            //  Loan loan = _context.Loans.Where(x=>x)


//            Customer customer = new Customer
//            {
//                //Streetaddress = model.Address.ToFirstLetterUpper(),
//                Cust_balance = 0,
//                //Telephonenumber = model.Phone,
//                //Surname = model.Surname.ToFirstLetterUpper(),
//                //Givenname = model.Givenname.ToFirstLetterUpper(),
//                //OtherNames = model.OtherNames.ToFirstLetterUpper(),
//                Deadline = DateTime.Now.AddMonths(model.Duration).ToString("yyyy-MM-dd"),
//                //Deadline = DateTime.Now.AddMonths(model.Duration).ToString("MM"),
//                CustAccNumber = AccountNumber,
//                //BVNNumber = model.BVNNumber,
//                //ImageThumbnailUrl = model.CustImageThumbnailUrl,
//                //UploadDocument = model.ScannThumbnailUrl,
//                //Birthday = model.Birthday,
//                Cust_acc_type = "Loan",
//                CreatedBy = LoginAdmin,
//                //Country = model.Country.ToFirstLetterUpper(),
//                //City = model.City.ToFirstLetterUpper(),
//                //Emailaddress = model.EmailAdress.ToFirstLetterUpper(),
//                //Gender = model.Gender,
//            };

//            // customer.Cust_acc_type = model.AccountType.ToFirstLetterUpper();

//            _context.Customers.Add(customer);


//            var account = new Account
//            {
//                Balance = 0m,
//                Created = DateTime.Now,
//                Frequency = AccountFrequency.Weekly,
//                AccountNumber = AccountNumber,
//                CustId = customer.Id
//            };
//            _context.Accounts.Add(account);

//            //if(model.Frequency == "Weekly")
//            //{
//            Loan loan = new Loan();

//            //loan.LoanCause = model.LoanCause;
//            loan.Loan_Amount = model.Loan_Amount;
//            loan.Deadline = DateTime.Now.AddMonths(model.Deadline).ToString("yyyy-MM-dd");
//            loan.Duration = model.Duration;
//            loan.Interest_Rate = model.Interest_Rate;
//            // loan.Deadline = DateTime.Now.AddYears(Loan_Deadline).ToString("yyyy-MM-dd");
//            loan.Frequency = model.Frequency;

//            loan.AmountTo_Pay = loan.Loan_Amount + ((loan.Loan_Amount * loan.Interest_Rate) / 100);

//            loan.IsActive = true;
//            loan.Loan_Amount_Paid = 0;
//            loan.Loan_Date = DateTime.Now.ToString("yyyy-MM-dd");
//            loan.Manager_Approval = "No";
//            loan.LOfficer_Id = Convert.ToInt32(loginId);
//            loan.AccountNumber = AccountNumber;
//            loan.AccountId = account.AccountId;
//            loan.CustId = customer.Id;

//            _context.Loans.Add(loan);


//            //}
//            //else if (model.Frequency == "Monthly")
//            //{

//            //}
//            //  throw new Exception("Loan Account Successfully Created");

//            Transaction tr = new Transaction();
//            tr.Amount = Convert.ToDecimal(model.Loan_Amount);
//            tr.Tr_Date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
//            tr.Tr_EmpType = "LOfficer";
//            tr.Tr_AccName = customer.Surname + " " + customer.Givenname + " " + customer.OtherNames;
//            tr.Type = "Loan";
//            tr.AccountNumber = AccountNumber;
//            // tr.AccountNumber = 

//            _context.Transactions.Add(tr);

//            await _context.SaveChangesAsync();
//            return true;
//        }

//    }
//}
