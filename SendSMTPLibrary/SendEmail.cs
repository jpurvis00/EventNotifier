using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace SendSMTPLibrary
{
    public class SendEmail
    {
        public void SendEmailMessage(string subject, string ourEvent, string toEmailAddresses)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("insert value", "insert value"));
            
            foreach (string toAddresses in toEmailAddresses.Split(','))
            {
                message.To.Add(new MailboxAddress("insert value", toAddresses));
            }
            
            message.Subject = subject;

            message.Body = new TextPart("plain")
            {
                Text = $"Event: {ourEvent}"
            };

            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 465, true);
                client.Authenticate("insert value", "insert value");

                client.Send(message);
                client.Disconnect(true);
            }
        }
    }        
}
