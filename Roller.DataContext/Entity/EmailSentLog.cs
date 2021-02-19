using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Roller.DataContext.Entity
{
    public class EmailSentLog: BaseEntity.Entity
    {
        public string RecipientEmail { get; set; }

        [DataType(DataType.Text)]
        public string EmailContent { get; set; }
        public string Status { get; set; }

        public string Sender { get; set; }
        public string Reciever { get; set; }
        public string Subject { get; set; }
        public string MessageBody { get; set; }
        public string SentDate { get; set; } 
        public string DateToSend { get; set; } 
        public bool IsSent { get; set; }
        public string Frequency { get; set; }
        public string  CopyTo { get; set; }
        public string  RecieverName { get; set; }
    }
}
