using Roller.Web.Utility;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Roller.Repository.CashierRepo
{
   public interface ICashierRepository
    {
        TransferModel GetCustToTransfFrm(string accountNumber);
       // bool TransferBalance(TransferModel model);
        Task<SenderModel> TransferBalance(TransferModel model);
    }
}
