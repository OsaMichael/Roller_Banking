using Hangfire.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roller.Web.Utility
{
    public class HangFireAuthorizationFilter:  IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext _context)
        {
            var httpContext = _context.GetHttpContext();
            return true;

        }
    }
}
