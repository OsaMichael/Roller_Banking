using System;
using System.Collections.Generic;
using System.Text;

namespace Roller.Repository.Extensions
{
  public  class InsufficientFundsException : Exception
    {
        public InsufficientFundsException(string name, object key)
           : base($"{name} #({key}) has insufficient funds.")
        {
        }
    }
}
