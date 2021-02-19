﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Roller.Repository
{
   public class ElasticEmailService : IEmailService
    {
        public async Task<string> Send(string to, string subject, string content)

            // Server name: pro.turbo - smtp.com
            //Server name(recommended for EU users): pro.eu.turbo - smtp.com
            //Username: webmaster @cyberspace.net.ng
            //Password: lwoZbaHG
            //Port: 25(default) or 587; if you use an SSL connection, choose port 465

            var values = dict.Select(v => new KeyValuePair<string, string>(v.Key, v.Value));

            //dict.TryAdd("to", to);
                //var contentt = new StringContent(dict.ToString(), Encoding.UTF8, "application/json");
                //var response = client.PostAsync(address, contentt).Result;
                //response.EnsureSuccessStatusCode();


                //var responseR = await response.Content.ReadAsStringAsync();
                var formContent = new FormUrlEncodedContent(values);
                var apiResponse = await client.PostAsync(address, formContent);
                apiResponse.EnsureSuccessStatusCode();
             var response = await apiResponse.Content.ReadAsStringAsync();
                //if (response.StatusCode == HttpStatusCode.OK)
                return response;
    }
}