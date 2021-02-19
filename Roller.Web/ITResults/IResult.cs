using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roller.Web.ITResults
{
    public interface IResult
    {
        bool IsSuccess { get; set; }
        string Error { get; set; }
        string Success { get; set; }
    }
}
