using System;
using System.Collections.Generic;
using System.Text;

namespace Roller.Repository.Services
{
    public interface IPaymentService
    {
        TransactionModel PostTransaction(TransactionModel model);
    }
}
