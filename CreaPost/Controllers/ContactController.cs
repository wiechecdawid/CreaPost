using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CreaPost.Models;
using CreaPost.Services;
using CreaPost.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CreaPost.Controllers
{
    [Route("Contact")]
    public class ContactController : Controller
    {
        private readonly ILogger _logger;

        private readonly IEmailSender _sender;

        public ContactController(ILogger<ContactController> logger, IHostingEnvironment env, IOptions<EmailSettings> options)
        {
            _logger = logger;
            _sender = new EmailSender(options, env);
        }

        [HttpGet]
        public IActionResult Contact()
        {
            var model = new ContactViewModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Contact(ContactViewModel model)
        {
            await _sender.SendEmailAsync(model.Email, model.Subject, model.Message);

            return Ok();
        }
    }
}