using System;
using System.Collections.Generic;
using System.Text;

namespace Roller.Repository.Extensions
{
  public  class NotFoundException : Exception
    {
        public NotFoundException(string name, object key)
     : base($"{name} ID {key} was not found.")
        {
        }
    }
}
