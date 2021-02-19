
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Roller.DataContext.Entity;
using Roller.Repository.EmailSentLogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Roller.Web.Utility
{
    public class EmailSender : IEmailSender
    {
        //TODO: Implement Email Sending function
        //NOTE: Send Email to successfully registered customer
        private IConfiguration _configuration { get; }
        private readonly EmailSettings _emailSettings;
        private readonly IEmailSentLog _emailSentLogRepo;
        private ILogger _logger { get; }

        public EmailSender(IConfiguration configuration, IOptions<EmailSettings> emailSettings, IEmailSentLog emailSentLogRepo, ILogger<EmailSender> logger)
        {
            _configuration = configuration;
            _logger = logger;
            _emailSettings = emailSettings.Value;
            _emailSentLogRepo = emailSentLogRepo;
        }

        public bool sendmail(string recipientEmail, string recepientName, string accountNumber/*, decimal amount*/)
        {
            bool send = false;
            try
            {
                // string Mail = _configuration["Mail"].ToString();
                string emailAddress = _configuration["EmailConfiguration:EmailAdress"];
                string name = _configuration["Properties:BankName"];
                StringBuilder mailBody = new StringBuilder();
                mailBody.AppendFormat("Welcome to bank {0}  , ", name);
                mailBody.AppendFormat("<br />");
                mailBody.AppendFormat("Dear {0},", recepientName);
                mailBody.AppendFormat("<br />");
                mailBody.AppendFormat("Transaction #{0} , ", accountNumber);
                mailBody.AppendFormat("<br />");
                mailBody.AppendFormat("Date {0} , ", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                mailBody.AppendFormat("<br />");
                //mailBody.AppendFormat("Transaction {0} amount", amount);
                mailBody.AppendFormat("<p>Thank you</p>");
                var body = mailBody.ToString();

                System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
                client.Port = 587;
                client.Host = "smtp.office365.com";
                client.EnableSsl = true;
                client.Timeout = 10000;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential("no-reply@cyberspace.net.ng", "Billing123","ROLLER");
                MailMessage mm = new MailMessage("no-reply@cyberspace.net.ng", recipientEmail, "TRANSACTION ALERT", body);
                mm.BodyEncoding = UTF8Encoding.UTF8;
                mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                mm.Body = body;
                mm.IsBodyHtml = true;


                client.Send(mm);
                send = true;
                return send;
            }

            catch
            {
                return send = false;
            }

            return true;
        }
        public async Task SendEmailAsync(string email, string subject, string message, string attachedfiles)
        {
            try
            {

                MailMessage mm = new MailMessage();

                foreach (string n in email.Split(','))
                {
                    if (!string.IsNullOrWhiteSpace(n)) mm.To.Add(new MailAddress(n.Trim()));
                }
                mm.Sender = new MailAddress(_emailSettings.SenderName);
                mm.From = new MailAddress(_emailSettings.SenderName);
                mm.Subject = subject;
                mm.Body = message;

                mm.BodyEncoding = UTF8Encoding.UTF8;
                mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;


                //attachements
                if (!string.IsNullOrWhiteSpace(attachedfiles))
                {
                    foreach (string attachm in attachedfiles.Split(';'))
                    {
                        if (!string.IsNullOrEmpty(attachm)) mm.Attachments.Add(new Attachment(attachm));
                    }
                }

                mm.BodyEncoding = UTF8Encoding.UTF8;
                mm.IsBodyHtml = true;
                mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

                EmailSentLog emailLog = new EmailSentLog
                {
                    EmailContent = message,
                    RecipientEmail = email
                };

                using (var client = new System.Net.Mail.SmtpClient())
                {

                    client.Port = _emailSettings.MailPort;
                    client.Host = _emailSettings.MailServer;
                    client.EnableSsl = _emailSettings.SSl;   //true
                    client.Timeout = 20000;
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.UseDefaultCredentials = false;
                    client.Credentials = new System.Net.NetworkCredential(_emailSettings.Sender, _emailSettings.Password);


                    // Note: only needed if the SMTP server requires authentication
                    //await client.AuthenticateAsync(_emailSettings.Sender, _emailSettings.Password);
                    try
                    {
                        await Task.Run(() =>
                           client.Send(mm)
                        );
                    }
                    catch (SmtpFailedRecipientsException ex)
                    {
                        for (int i = 0; i < ex.InnerExceptions.Length; i++)
                        {
                            System.Net.Mail.SmtpStatusCode status = ex.InnerExceptions[i].StatusCode;
                            if (status == System.Net.Mail.SmtpStatusCode.MailboxBusy ||
                                status == System.Net.Mail.SmtpStatusCode.MailboxUnavailable)
                            {
                                //Console.WriteLine("Delivery failed - retrying in 5 seconds.");
                                //System.Threading.Thread.Sleep(5000);
                                client.Send(mm);
                            }
                            else
                            {
                                //Console.WriteLine("Failed to deliver message to {0}",
                                //    ex.InnerExceptions[i].FailedRecipient);

                                emailLog.Status = "Failed";
                            }
                        }
                    }

                    await _emailSentLogRepo.LogEmailAsync(emailLog);
                    // await client.DisconnectAsync(true);
                }

            }
            catch (Exception ex)
            {
                // TODO: handle exception
                throw new InvalidOperationException(ex.Message);
            }
        }


        //public bool SendEmail(string recepientName, string recipientEmail, string accountNumber/*, int verificationCode*/)
        //{
        //    string name = _configuration["Properties:BankName"];
        //    string emailAddress = _configuration["EmailConfiguration:EmailAdress"];

        //    var Message = new MimeMessage();
        //    Message.From.Add(new MailboxAddress("Michael Aruebo", emailAddress));
        //    Message.To.Add(new MailboxAddress(recepientName, recipientEmail));
        //    Message.Subject = "Roller Account Details";

        //    var builder = new BodyBuilder
        //    {
        //        TextBody = (@"
        //                        Welcome to " + name + " bank," +
        //                        "Account Number: " + accountNumber + "." +
        //                        //"Verification Code: " + verificationCode + "." +
        //                        "Thank you for Banking with us" +
        //                        "Customer service: 01-1234556 "
        //                        ),
        //        HtmlBody = (@"
        //                        Welcome to " + name + " bank,<br>" +
        //                        "Account Number: " + accountNumber + ".<br>" +
        //                        //"Verification Code: " + verificationCode + ".<br>" +
        //                        "Thank you for Banking with us<br><br>" +
        //                        "<b>Customer service: 01-1234556</b> ")
        //    };

        //    Message.Body = builder.ToMessageBody();
        //using (var client = new SmtpClient())
        //{
        //    client.Connect("smtp.gmail.com", 587);

        //    // Note: since we don't have an OAuth2 token, disable
        //    // the XOAUTH2 authentication mechanism.
        //    client.AuthenticationMechanisms.Remove("XOAUTH2");

        //    // Note: only needed if the SMTP server requires authentication
        //    //client.Authenticate("Michael Aruebo", _configuration["EmailConfiguration:Password"]);

        //    try
        //    {
        //        client.Send(Message);
        //        client.Disconnect(true);
        //    }
        //    catch (Exception e)
        //    {
        //        _logger.LogError(LogEvents.EmailServerError, e, e.Message, Message);
        //        return false;
        //    }

        //    return true;
        //}

        //}


    }

}    

