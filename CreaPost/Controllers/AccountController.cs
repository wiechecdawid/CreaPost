using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CreaPost.Models;
using CreaPost.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace CreaPost.Controllers
{
    public class AccountController : Controller
    {
        private readonly IEmailSender _sender;
        private readonly ILoggerFactory _logger;
        private readonly SignInManager<StoreUser> _signInManager;
        private readonly UserManager<StoreUser> _userManager;
        private readonly IConfiguration _configuration;

        public AccountController(ILoggerFactory logger, 
                                    SignInManager<StoreUser> signInManager,
                                    UserManager<StoreUser> userManager,
                                    IConfiguration configuration,
                                   IEmailSender sender)
        {
            _sender = sender;
            _logger = logger;
            _signInManager = signInManager;
            _userManager = userManager;
            _configuration = configuration;
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            var logger = _logger.CreateLogger("LoggerCategory");

            if (ModelState.IsValid)
            {
                var user = new StoreUser
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    UserName = model.Email
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    //await _signInManager.SignInAsync(user, false);
                    //return RedirectToAction("Index", "Home");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userEmail = user.Email, code = code }, protocol: Request.Scheme);
                    var subject = "Crea Post User Confirmation";
                    var message = "Hello," + Environment.NewLine + "Thank you for registering into our website. We hope you will enjoy your time here"
                                + Environment.NewLine + "We need one more step to enable your service. Please click on the confirmation link below"
                                + Environment.NewLine + "<a href=\"" + callbackUrl + "\">LINK<a>";


                    await _sender.SendEmailAsync(user.Email.ToString(), subject, message);
                    return View("DisplayEmail");
                }                    
                else
                {
                    foreach (var error in result.Errors)
                        ModelState.AddModelError("error", error.Description);
                }
            }
                    
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            if(this.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Login(AccountViewModel model)
        {
            if(ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email,
                    model.Password,
                    model.RememberMe,
                    false);

                if (result.Succeeded)
                {
                    if (Request.Query.Keys.Contains("ReturnUrl"))
                        return Redirect(Request.Query["ReturnUrl"].First());
                    else
                        return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", "Failed to Log in");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> ConfirmEmail(string userEmail, string code)
        {
            if(userEmail==null || code==null)
            {
                return BadRequest();
            }

            var user = await _userManager.FindByEmailAsync(userEmail);

            var result = await _userManager.ConfirmEmailAsync(user, code);

            string tempMessage = "You have successfully confirmed your email address";
            TempData["Message"] = tempMessage;

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateToken([FromBody] AccountViewModel model)
        {
            if(ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);

                if(user!=null)
                {
                    var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

                    if (result.Succeeded)
                    {
                        var claims = new[]
                        {
                        new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName)
                        };

                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:Key"]));

                        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                        var token = new JwtSecurityToken(
                            _configuration["Tokens:Issuer"],
                            _configuration["Tokens:Audience"],
                            claims,
                            expires: DateTime.UtcNow.AddMinutes(30),
                            signingCredentials: credentials
                            );

                        var results = new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(token),
                            expiration = token.ValidTo
                        };
                        return Created("", results);
                    }
                }
            }
            return BadRequest();
        }
    }
}