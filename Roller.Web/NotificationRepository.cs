﻿//using AutoMapper.Configuration;
//using Microsoft.Extensions.Options;
//using Roller.Web.Utility;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net.Http;
//using System.Text;
//using System.Threading.Tasks;

//namespace Roller.Repository
//{
//  public  class NotificationRepository: INotificationRepository
//    {

//        private readonly EmailSettings _emailSettings;
//        private readonly IConfiguration _configuration;

//        public NotificationRepository(IConfiguration configuration, IOptions<EmailSettings> emailSettings)
//        {
//            _emailSettings = emailSettings.Value;
//            _configuration = configuration;
//        }

//        public async Task<string> Send(string to, string subject, string content)

//            var values = dict.Select(v => new KeyValuePair<string, string>(v.Key, v.Value));
//    }

//}
