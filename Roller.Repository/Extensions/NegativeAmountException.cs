using System;
using System.Collections.Generic;
using System.Text;

namespace Roller.Repository.Extensions
{
  public  class NegativeAmountException: Exception
    {
        public NegativeAmountException(string name, object key)
           : base("Can't transfer a negative amount.")
        {
        }
    }
}
