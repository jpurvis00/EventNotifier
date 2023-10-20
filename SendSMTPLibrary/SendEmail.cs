using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using DataAccessLibrary.Options;

namespace SendSMTPLibrary
{
    public class SendEmail
    {
        public void SendEmailMessage(string subject, string ourEvent, string toEmailAddresses, EmailSettingsOptions emailSettings)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(emailSettings.FromMailboxAddressName, emailSettings.FromMailboxAddressAddress));

            foreach (string toAddresses in toEmailAddresses.Split(','))
            {
                message.To.Add(new MailboxAddress(emailSettings.AddMailboxAddress, toAddresses));
            }

            message.Subject = subject;

            message.Body = new TextPart("plain")
            {
                Text = $"Event: {ourEvent}"
            };

            using (var client = new SmtpClient())
            {
                client.Connect(emailSettings.SmtpHost, emailSettings.SmtpPort, emailSettings.SmtpUseSSL);
                //client.Authenticate(emailSettings.AuthenticateUserName, emailSettings.AuthenticatePw);
                client.Authenticate(emailSettings.AuthenticateUserName, emailSettings.AuthenticatePw);

                client.Send(message);
                client.Disconnect(true);
            }
        }
    }        
}
