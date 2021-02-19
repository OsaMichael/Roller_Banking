using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roller.Web.Controllers.Common
{
    public class Enum
    {
        public enum NotificationType
        {
            error,
            success,
            warning,
            info
        }
        public enum MessageType
        {
            ErrorMessage,
            SuccessMessage,
            InfoMessage,
            Warning,
        }

    }
}
