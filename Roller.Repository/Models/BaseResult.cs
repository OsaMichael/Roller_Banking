using System;
using System.Collections.Generic;
using System.Text;

namespace Roller.Repository.Models
{
   public class BaseResult : IResult
    {
        public bool IsSuccess { get; set; }
        public string Success { get; set; }
        public string Error { get; set; }
    }
}
