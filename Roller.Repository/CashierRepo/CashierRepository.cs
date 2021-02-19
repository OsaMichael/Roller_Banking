using Roller.DataContext.Entity;
using Roller.DContext;
using Roller.Web.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roller.Repository.CashierRepo
{
   public class CashierRepository: ICashierRepository
    {
        private readonly RollerDataContext _context;
        public CashierRepository(RollerDataContext context)
        {
            _context = context;
        }
        public List<Cashier> GetAll()
        {
            return _context.Cashiers.ToList();
        }
        public Cashier Get(int Cashier_Id)
        {
            return _context.Cashiers.SingleOrDefault(d => d.Cashier_Id == Cashier_Id);
        }
        public TransferModel GetCustToTransfFrm(string accountNumber)
        {
            var accountNo = _context.Customers.Where(c => c.CustAccNumber == accountNumber).FirstOrDefault();
             if (accountNo == null) throw new Exception("user not found");

            var result = new TransferModel
            {
                AccountNumber_from = accountNo.CustAccNumber,
                FullName = accountNo.Surname +  " " + accountNo.Givenname + "-" + accountNo.CustAccNumber
                 //AccoutType = "S"
            };

            return result;

        }

        public async Task<SenderModel> TransferBalance(TransferModel model)
        {
            //  Loan loan = _context.Loans.Where(x=>x)
            try
            {

                Customer tranFr =  _context.Customers.SingleOrDefault(x => x.CustAccNumber == model.AccountNumber_from);
                Account accountFr = _context.Accounts.SingleOrDefault(x => x.AccountNumber == model.AccountNumber_from);

                Customer tranTo = _context.Customers.SingleOrDefault(x => x.CustAccNumber == model.AccountNumber_to);
                Account accountTo = _context.Accounts.SingleOrDefault(x => x.AccountNumber == model.AccountNumber_to);

                if (tranFr.Cust_balance > model.Amount + 1000 && tranTo.Cust_acc_type == "Savings")
                {
                    tranFr.Cust_balance -= model.Amount;
                    tranTo.Cust_balance += model.Amount;
                    //_context.SaveChanges();

                    accountFr.Balance = tranFr.Cust_balance;
                    accountTo.Balance = tranTo.Cust_balance;

                  
                    Transaction tr = new Transaction();
                    tr.Amount = Convert.ToDecimal(model.Amount);
                    tr.Tr_Date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    tr.Tr_EmpType = "Cashier";
                    tr.Tr_AccName = tranFr.Surname + " " + tranFr.Givenname + " " + tranFr.OtherNames;
                    //tr.Tr_AccName = tranTo.Surname + " " + tranTo.Givenname + " " + tranTo.OtherNames;
                    tr.Type = "Savings";
                    tr.CustomerId = tranFr.Id;
                    tr.AccountNumber = tranFr.CustAccNumber;
                    //tr.AccountNumber = tranTo.CustAccNumber;
                    // tr.AccountNumber = 

                    _context.Transactions.Add(tr);
                  await  _context.SaveChangesAsync();
                }
                else if (tranFr.Cust_balance > model.Amount + 1000 && tranTo.Cust_acc_type == "Current")
                {
                    ///////////////////////////////////////////////////////////////////////////
                }

                var mail = new SenderModel
                {
                    accountNumber = tranFr.CustAccNumber,
                    recepientName = tranFr.Surname + " " + tranFr.Givenname,
                    recipientEmail = tranFr.Emailaddress,
                    Amount = model.Amount,
                    Date = tranFr.LastDateUpdated.ToString(),
                        recievedMails = new RecievedMail
                        {
                             accountNumber = tranFr.CustAccNumber,
                            recepientName = tranTo.Surname + " " + tranTo.Givenname,
                               recipientEmail = tranTo.Emailaddress,
                               Amount = model.Amount,
                                Date = tranTo.LastDateUpdated.ToString(),
                        }                    
                };
                return mail;
            }
            catch (Exception xx)
            {
                throw xx;
            }
        }

        public bool CreateCategory(CashierModel model)
        {
            var isExist = _context.Cashiers.Where(x => x.Cashier_Name == model.Cashier_Name).Count();
            if (isExist != 0) throw new Exception("item already exist");

            Cashier cashiers = new Cashier();

            if (isExist == 0)
            {
                cashiers.Cashier_Name = model.Cashier_Name;
                //category.CategoryName = model.CategoryName;
                //category.CreatedDate = DateTime.Now;
                //category.CreatedBy = model.CreatedBy;

                _context.Add(cashiers);
                _context.SaveChanges();

              //  Message = "Category created successfully";

                return true;
            }
            else
            {
               // Message = "Category already exist";

                return false;
            }

        }
    }
}
