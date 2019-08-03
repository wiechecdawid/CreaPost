using CreaPost.Models;
using MailKit.Net.Smtp;
using MailKit;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using MimeKit;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreaPost.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailSettings _options;
        private readonly IHostingEnvironment _env;

        public EmailSender(IOptions<EmailSettings> options,
                            IHostingEnvironment env)
        {
            _options = options.Value;
            _env = env;
        }
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {            
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("creapost2019", "creapost2019@gmail.com"));
                message.To.Add(new MailboxAddress(email));
                message.Subject = subject;
                message.Body = new TextPart("html")
                {
                    Text = htmlMessage
                };

                using(var client = new SmtpClient())
                {
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                    if(_env.IsDevelopment())
                    {
                        await client.ConnectAsync("smtp.gmail.com", 465, true);
                    }
                    else
                    {
                        await client.ConnectAsync("smtp.gmail.com");
                    }

                    await client.AuthenticateAsync("creapost2019@gmail.com", "EVxu2qKab4yfQzi");
                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);
                }
            }
            catch(Exception ex)
            {
                string exMessage;
                //                var message=$"Failed to log in. Error: {ex.Message}";
                if (email == null)
                {
                    exMessage = "Email cannot be null";
                    throw new InvalidOperationException(exMessage);
                }
                    
                else if (subject == null)
                {
                    exMessage = "Subject cannot be null";
                    throw new InvalidOperationException(exMessage);
                }
                    
                else if (htmlMessage == null)
                {
                    exMessage = "Subject cannot be null";
                    throw new InvalidOperationException(exMessage);
                }
                    
                else
                throw new InvalidOperationException(ex.Message);
            }
        }
    }
}
