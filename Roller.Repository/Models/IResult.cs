using System;
using System.Collections.Generic;
using System.Text;

namespace Roller.Repository.Models
{
   public interface IResult
    {
        bool IsSuccess { get; set; }
        string Error { get; set; }
        string Success { get; set; }
    }
}
