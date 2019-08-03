using CreaPost.Models;
using CreaPost.Services;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreaPost.Middleware
{
    public static class ServiceCollectionExtensions
    {
        public static void CustomEmailConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var emailSettings = configuration.GetSection("EmailSettings");
            var settings = emailSettings.Get<EmailSettings>();

            services.Configure<EmailSettings>(options =>
            {
                options.MailServer = emailSettings.GetValue<string>("MailServer");
                options.MailPort = emailSettings.GetValue<int>("MailPort");
                options.SenderName = emailSettings.GetValue<string>("SenderName");
                options.Sender = emailSettings.GetValue<string>("Sender");
                options.Password = emailSettings.GetValue<string>("Password");
            });
            services.AddTransient<IEmailSender, EmailSender>();
        }
    }
}
