﻿using Roller.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Roller.Repository
{
    public interface INotification : IDependencyRegister
    {
       // Task<string> Send(string to, string subject, string content);
        Task SendMaill();
    }
}
