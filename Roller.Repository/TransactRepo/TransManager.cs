using Microsoft.EntityFrameworkCore;
using Roller.DataContext.Entity;
using Roller.DContext;
using Roller.Repository.Extensions;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roller.Repository.TransactRepo
{
   public class TransManager : ITransManager
    {
        private readonly RollerDataContext _context;

        public TransManager(RollerDataContext context)
        {
            _context = context;
        }
        public IEnumerable<Transaction> GetAll()
        {
            var entity = _context.Transactions.ToList();

            return entity;
        }
        public int Insert(Transaction Tr)
        {
            _context.Transactions.Add(Tr);
            return _context.SaveChanges();
        }

        public List<Transaction> GetByType(string Tr_Type, string createdBy)
        {
           // DataAccess db = new DataAccess();
            //string sql = "Select * from Transactions where Tr_Type='" + Tr_Type + "'";

            var result = _context.Transactions.Where(c => c.Type == Tr_Type).ToList();
            List<Transaction> ll = new List<Transaction>();
            Transaction l = new Transaction();

            l.TransactionId = Convert.ToInt32( result.Select(c => c.TransactionId));
            l.Tr_AccName = result.Select(n => n.Tr_AccName).ToString();
            l.Type = result.Select(n => n.Type).ToString();
            l.Tr_Through = createdBy;
            l.Amount = Convert.ToInt32(result.Select(v => v.Amount));
            l.Date = Convert.ToDateTime(result.Select(c => c.Date));
            l.Tr_EmpType = createdBy;
            l.Tr_Branch = result.Select(x => x.Tr_Branch).ToString();

            ll.Add(l);
           // SqlDataReader data = db.GetData(sql);

            //while (data.Read())
            //{
            //    Transaction l = new Transaction();
            //    l.Tr_Id = Convert.ToInt32(data["Tr_Id"]);
            //    l.Tr_Through = data["Tr_Through"].ToString();
            //    l.Tr_AccName = data["Tr_AccName"].ToString();
            //    l.Tr_Type = data["Tr_Type"].ToString();
            //    l.Tr_Amount = Convert.ToInt32(data["Tr_Amount"]);
            //    l.Tr_Date = data["Tr_Date"].ToString();
            //    l.Tr_EmpType = data["Tr_EmpType"].ToString();
            //    l.Tr_Branch = data["Tr_Branch"].ToString();

            //    ll.Add(l);
            //}

            return ll;
        }

        public List<Transaction> GetByEmp(string Tr_Through, string createdby)
        {
            //DataAccess db = new DataAccess();
            //string sql = "Select * from Transactions where Tr_Through='" + Tr_Through + "'";
            //List<Transaction> ll = new List<Transaction>();
            var result = _context.Transactions.Where(c => c.Tr_Through == Tr_Through).ToList();
            List<Transaction> ll = new List<Transaction>();
            Transaction l = new Transaction();

            l.TransactionId = Convert.ToInt32(result.Select(c => c.TransactionId));
            l.Tr_AccName = result.Select(n => n.Tr_AccName).ToString();
            l.Type = result.Select(n => n.Type).ToString();
            l.Tr_Through = Tr_Through;
            l.Amount = Convert.ToInt32(result.Select(v => v.Amount));
            l.Date = Convert.ToDateTime(result.Select(c => c.Date));
            l.Tr_EmpType = createdby;
            l.Tr_Branch = result.Select(x => x.Tr_Branch).ToString();

            ll.Add(l);

            //  SqlDataReader data = db.GetData(sql);

            //while (data.Read())
            //{
            //    Transaction l = new Transaction();
            //    l.Tr_Id = Convert.ToInt32(data["Tr_Id"]);
            //    l.Tr_Through = data["Tr_Through"].ToString();
            //    l.Tr_AccName = data["Tr_AccName"].ToString();
            //    l.Tr_Type = data["Tr_Type"].ToString();
            //    l.Tr_Amount = Convert.ToInt32(data["Tr_Amount"]);
            //    l.Tr_Date = data["Tr_Date"].ToString();
            //    l.Tr_EmpType = data["Tr_EmpType"].ToString();
            //    l.Tr_Branch = data["Tr_Branch"].ToString();

            //    ll.Add(l);
            //}

            return ll;
        }

        public List<Transaction> GetByPos(string Tr_EmpType, string createdby)
        {
                var result = _context.Transactions.Where(c => c.Tr_EmpType == Tr_EmpType).ToList();
            List<Transaction> ll = new List<Transaction>();
            Transaction l = new Transaction();

            l.TransactionId = Convert.ToInt32(result.Select(c => c.TransactionId));
            l.Tr_AccName = result.Select(n => n.Tr_AccName).ToString();
            l.Type = result.Select(n => n.Type).ToString();
            l.Tr_Through = createdby;
            l.Amount = Convert.ToInt32(result.Select(v => v.Amount));
            l.Date = Convert.ToDateTime(result.Select(c => c.Date));
            l.Tr_EmpType = createdby;
            l.Tr_Branch = result.Select(x => x.Tr_Branch).ToString();

            ll.Add(l);

            return ll;
        }

        public List<Transaction> GetToday(string createdby)
        {
            //DataAccess db = new DataAccess();
            //string sql = "Select * from Transactions where Tr_Date ='" + DateTime.Today.ToString("yyyy-MM-dd") + "'";
            //List<Transaction> ll = new List<Transaction>();
            var result = _context.Transactions.Where(c => c.Tr_Date == DateTime.Today.ToString("yyyy-MM-dd")).ToList();
            List<Transaction> ll = new List<Transaction>();
            Transaction l = new Transaction();


            l.TransactionId = Convert.ToInt32(result.Select(c => c.TransactionId));
            l.Tr_AccName = result.Select(n => n.Tr_AccName).ToString();
            l.Type = result.Select(n => n.Type).ToString();
            l.Tr_Through = createdby;
            l.Amount = Convert.ToInt32(result.Select(v => v.Amount));
            l.Date = Convert.ToDateTime(result.Select(c => c.Date));
            l.Tr_EmpType = createdby;
            l.Tr_Branch = result.Select(x => x.Tr_Branch).ToString();

            ll.Add(l);
            

            return ll;
        }

        public List<Transaction> GetToday(string Manager_branch, string createdby)
        {


           // DataAccess db = new DataAccess();
            //string sql = "Select * from Transactions where Tr_Date ='" + DateTime.Today.ToString("yyyy-MM-dd") + "' AND Tr_Branch='" + Manager_branch + "'";
            var result = _context.Transactions.Where(c => c.Tr_Date == DateTime.Today.ToString("yyyy-MM-dd") && c.Tr_Branch == Manager_branch).ToList();
            List<Transaction> ll = new List<Transaction>();
            Transaction l = new Transaction();


            l.TransactionId = Convert.ToInt32(result.Select(c => c.TransactionId));
            l.Tr_AccName = result.Select(n => n.Tr_AccName).ToString();
            l.Type = result.Select(n => n.Type).ToString();
            l.Tr_Through = createdby;
            l.Amount = Convert.ToInt32(result.Select(v => v.Amount));
            l.Date = Convert.ToDateTime(result.Select(c => c.Date));
            l.Tr_EmpType = createdby;
            l.Tr_Branch = result.Select(x => x.Tr_Branch).ToString();

            ll.Add(l);


            return ll;
        }

        public List<Transaction> GetYesterday(string createdby)
        {
          //  DataAccess db = new DataAccess();
           // string sql = "Select * from Transactions where Tr_Date = '" + DateTime.Today.AddDays(-1).ToString("yyyy-MM-dd") + "'";

            var result = _context.Transactions.Where(c => c.Tr_Date == DateTime.Today.AddDays(-1).ToString("yyyy-MM-dd")).ToList();
            List<Transaction> ll = new List<Transaction>();
            Transaction l = new Transaction();

            l.TransactionId = Convert.ToInt32(result.Select(c => c.TransactionId));
            l.Tr_AccName = result.Select(n => n.Tr_AccName).ToString();
            l.Type = result.Select(n => n.Type).ToString();
            l.Tr_Through = createdby;
            l.Amount = Convert.ToInt32(result.Select(v => v.Amount));
            l.Date = Convert.ToDateTime(result.Select(c => c.Date));
            l.Tr_EmpType = createdby;
            l.Tr_Branch = result.Select(x => x.Tr_Branch).ToString();

            ll.Add(l);

            return ll;
        }

        public List<Transaction> GetYesterday(string Manager_branch, string createdby)
        {
          //  DataAccess db = new DataAccess();
           // string sql = "Select * from Transactions where Tr_Date = '" + DateTime.Today.AddDays(-1).ToString("yyyy-MM-dd") + "' and Tr_Branch='" + Manager_branch + "'";
            var result = _context.Transactions.Where(c => c.Tr_Date == DateTime.Today.AddDays(-1).ToString("yyyy-MM-dd") && c.Tr_Branch == Manager_branch).ToList();
            List<Transaction> ll = new List<Transaction>();
            Transaction l = new Transaction();

            l.TransactionId = Convert.ToInt32(result.Select(c => c.TransactionId));
            l.Tr_AccName = result.Select(n => n.Tr_AccName).ToString();
            l.Type = result.Select(n => n.Type).ToString();
            l.Tr_Through = createdby;
            l.Amount = Convert.ToInt32(result.Select(v => v.Amount));
            l.Date = Convert.ToDateTime(result.Select(c => c.Date));
            l.Tr_EmpType = createdby;
            l.Tr_Branch = result.Select(x => x.Tr_Branch).ToString();

            ll.Add(l);


            return ll;
        }


        public List<Transaction> Get6Months(string createdby)
        {
           // DataAccess db = new DataAccess();

            int year = DateTime.Now.Year;
            DateTime firstDay = new DateTime(year, 1, 1);
            DateTime lastDay = new DateTime(year, 06, 30);


            // string sql = "SELECT* from Transactions where Tr_Date BETWEEN '" + firstDay.ToString("yyyy-MM-dd") + "' AND '" + lastDay.ToString("yyyy-MM-dd") + "'";
            var result = _context.Transactions.Where(c => c.Tr_Date == firstDay.ToString("yyyy-MM-dd") && c.Tr_Date == lastDay.ToString("yyyy-MM-dd")).ToList();

            List<Transaction> ll = new List<Transaction>();
            Transaction l = new Transaction();

            l.TransactionId = Convert.ToInt32(result.Select(c => c.TransactionId));
            l.Tr_AccName = result.Select(n => n.Tr_AccName).ToString();
            l.Type = result.Select(n => n.Type).ToString();
            l.Tr_Through = createdby;
            l.Amount = Convert.ToInt32(result.Select(v => v.Amount));
            l.Date = Convert.ToDateTime(result.Select(c => c.Date));
            l.Tr_EmpType = createdby;
            l.Tr_Branch = result.Select(x => x.Tr_Branch).ToString();

            ll.Add(l);

            return ll;
        }

        public List<Transaction> Get6Months(string Manager_branch, string createdby)
        {
            //DataAccess db = new DataAccess();

            int year = DateTime.Now.Year;
            DateTime firstDay = new DateTime(year, 1, 1);
            DateTime lastDay = new DateTime(year, 06, 30);


           // string sql = "SELECT* from Transactions where (Tr_Date BETWEEN '" + firstDay.ToString("yyyy-MM-dd") + "'AND '" + lastDay.ToString("yyyy-MM-dd") + "' ) AND Tr_Branch='" + Manager_branch + "'";

            var result = _context.Transactions.Where(c => c.Tr_Date == firstDay.ToString("yyyy-MM-dd") && c.Tr_Date == lastDay.ToString("yyyy-MM-dd") && c.Tr_Branch == Manager_branch).ToList();

            List<Transaction> ll = new List<Transaction>();
            Transaction l = new Transaction();

            l.TransactionId = Convert.ToInt32(result.Select(c => c.TransactionId));
            l.Tr_AccName = result.Select(n => n.Tr_AccName).ToString();
            l.Type = result.Select(n => n.Type).ToString();
            l.Tr_Through = createdby;
            l.Amount = Convert.ToInt32(result.Select(v => v.Amount));
            l.Date = Convert.ToDateTime(result.Select(c => c.Date));
            l.Tr_EmpType = createdby;
            l.Tr_Branch = result.Select(x => x.Tr_Branch).ToString();

            ll.Add(l);

            return ll;
        }

        public List<Transaction> GetCurrentYear( string createdby)
        {
          //  DataAccess db = new DataAccess();

            int year = DateTime.Now.Year;
            DateTime firstDay = new DateTime(year, 1, 1);
            DateTime lastDay = new DateTime(year, 12, 31);

           // string sql = "SELECT* from Transactions where Tr_Date BETWEEN '" + firstDay.ToString("yyyy-MM-dd") + "'AND '" + lastDay.ToString("yyyy-MM-dd") + "'";
            var result = _context.Transactions.Where(c => c.Tr_Date == firstDay.ToString("yyyy-MM-dd") && c.Tr_Date == lastDay.ToString("yyyy-MM-dd")).ToList();

            List<Transaction> ll = new List<Transaction>();
            Transaction l = new Transaction();

            l.TransactionId = Convert.ToInt32(result.Select(c => c.TransactionId));
            l.Tr_AccName = result.Select(n => n.Tr_AccName).ToString();
            l.Type = result.Select(n => n.Type).ToString();
            l.Tr_Through = createdby;
            l.Amount = Convert.ToInt32(result.Select(v => v.Amount));
            l.Date = Convert.ToDateTime(result.Select(c => c.Date));
            l.Tr_EmpType = createdby;
            l.Tr_Branch = result.Select(x => x.Tr_Branch).ToString();

            ll.Add(l);
            return ll;
        }

        public List<Transaction> GetCurrentYear(string Manager_branch, string createdby)
        {
          //  DataAccess db = new DataAccess();

            int year = DateTime.Now.Year;
            DateTime firstDay = new DateTime(year, 1, 1);
            DateTime lastDay = new DateTime(year, 12, 31);


           // string sql = "SELECT* from Transactions where (Tr_Date BETWEEN '" + firstDay.ToString("yyyy-MM-dd") + "'AND '" + lastDay.ToString("yyyy-MM-dd") + "' ) AND Tr_Branch='" + Manager_branch + "'";
            var result = _context.Transactions.Where(c => c.Tr_Date == firstDay.ToString("yyyy-MM-dd") && c.Tr_Date == lastDay.ToString("yyyy-MM-dd") && c.Tr_Branch == Manager_branch).ToList();

            List<Transaction> ll = new List<Transaction>();
            Transaction l = new Transaction();

            l.TransactionId = Convert.ToInt32(result.Select(c => c.TransactionId));
            l.Tr_AccName = result.Select(n => n.Tr_AccName).ToString();
            l.Type = result.Select(n => n.Type).ToString();
            l.Tr_Through = createdby;
            l.Amount = Convert.ToInt32(result.Select(v => v.Amount));
            l.Date = Convert.ToDateTime(result.Select(c => c.Date));
            l.Tr_EmpType = createdby;
            l.Tr_Branch = result.Select(x => x.Tr_Branch).ToString();

            ll.Add(l);

            return ll;
        }

        public async Task<TransViewModel> GetTransactions(TransViewModel model)
        {
            var transactions = _context.Transactions
               .Where(t => t.CustomerId == model.AccountId)
               .OrderByDescending(t => t.TransactionId)
               .AsNoTracking().AsQueryable();

            var totalCount = transactions.Count();

            var modeli = new TransViewModel
            {
                Total = totalCount,
                NumberOfPages = totalCount / model.Limit - 1,
                HasMorePages = model.Offset + model.Limit < totalCount,

                Transactions = await transactions.Skip(model.Offset).Take(model.Limit).Select(t => new TransactionModel
                {
                  Balance = t.Balance.ToSwedishKrona(),
                  Operation = t.Operation,
                  Amount = t.Amount.ToSwedishKrona(),
                  Type = t.Type,
                  Date = t.Date,
                  Symbol = t.Symbol
                }).ToListAsync()

            };
            return model;
        }

        public TransViewModel GetTranApi(int id)
        {
            var youID =  _context.Accounts.Where(c => c.AccountId == id);
            return new TransViewModel { AccountId = Convert.ToInt32(youID) };
        }
        public TransViewModel GetTranApiIds(int id, int offset, int limit)
        {
            var youID = _context.Accounts.Where(c => c.AccountId == id);

           return new  TransViewModel
           {
               AccountId = Convert.ToInt32(youID),
                Limit = limit,
                 Offset = offset
           };
        }
          
        }
    }

