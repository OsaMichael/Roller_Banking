using Roller.DataContext.Entity;
using Roller.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Roller.Repository.TransactRepo
{
    public interface ITransManager: IDependencyRegister
    {
        Task<TransViewModel> GetTransactions(TransViewModel model);
        TransViewModel GetTranApi(int id);
        TransViewModel GetTranApiIds(int id, int offset, int limit);
        int Insert(Transaction Tr);
        IEnumerable<Transaction> GetAll();
        List<Transaction> GetByType(string Tr_Type, string createdBy);
        //  List<Transaction> GetByEmp(string Tr_Through);
        List<Transaction> GetByEmp(string Tr_Through, string createdby);
        List<Transaction> GetByPos(string Tr_EmpType, string createdby);
        List<Transaction> GetToday(string createdby);
        List<Transaction> GetToday(string Manager_branch, string createdby);
        List<Transaction> GetYesterday(string createdby);
        List<Transaction> GetYesterday(string Manager_branch, string createdby);
        List<Transaction> Get6Months(string createdby);
        List<Transaction> Get6Months(string Manager_branch, string createdby);
        List<Transaction> GetCurrentYear(string createdby);
        List<Transaction> GetCurrentYear(string Manager_branch, string createdby);
    }
}
