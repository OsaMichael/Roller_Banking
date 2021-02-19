using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Roller.Repository.Customers;
using static Roller.Web.Controllers.Common.Enum;

namespace Roller.Web.Controllers
{
    public abstract class BaseController : Controller
    {
        public void Alert(string message, dynamic content, NotificationType notificationType)
        {
            var msg = "<script language='javascript'>swal('" + notificationType.ToString().ToUpper() + "','" + content +"', '" + message + "','" + notificationType + "')" + " </script>";
            TempData["notification"] = msg;
            
        }
        
    }
}