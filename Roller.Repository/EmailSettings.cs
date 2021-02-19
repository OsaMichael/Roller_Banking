
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Roller.Web.Utility
{
    public class EmailSettings
    {
        public string From { get; set; }
        public string FromName { get; set; }
        public string ApiKey { get; set; }
        public string Url { get; set; }


        public bool SSl { get; set; } = false;
        public string MailServer { get; set; }
        public int MailPort { get; set; }
        public string Sender { get; set; }
        public string SenderName { get; set; }
        public string Password { get; set; }
        public string subject { get; set; }

        //public async Task<string> Send(string to, string subject, string content)        //{        //    var dict = new Dictionary<string, string>        //    {        //        { "apikey", "5b8a6fd9-8e07-4748-affa-0dcc39bf6754" },        //        { "from", "amichaelmusic10@gmail.com" },        //        { "fromName", "Roller" },        //        { "to", to },        //        { "subject", subject },        //        { "bodyHtml", content },        //        { "isTransactional", "true" }        //    };

        //    // Server name: pro.turbo - smtp.com
        //    //Server name(recommended for EU users): pro.eu.turbo - smtp.com
        //    //Username: webmaster @cyberspace.net.ng
        //    //Password: lwoZbaHG
        //    //Port: 25(default) or 587; if you use an SSL connection, choose port 465



        //    var values = dict.Select(v => new KeyValuePair<string, string>(v.Key, v.Value));        //    string address = "https://api.elasticemail.com/v2/email/send";        //    using (HttpClient client = new HttpClient())        //    {        //        var formContent = new FormUrlEncodedContent(values);        //        var apiResponse = await client.PostAsync(address, formContent);        //        apiResponse.EnsureSuccessStatusCode();        //        var response = await apiResponse.Content.ReadAsStringAsync();        //        return response;        //    }        //}
    }
}
