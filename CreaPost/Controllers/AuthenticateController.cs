using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CreaPost.Models.TokenValidationModels;
using CreaPost.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CreaPost.Controllers
{
    public class AuthenticateController : Controller
    {
        private readonly IAuthenticateService _authenticateService;

        public AuthenticateController(IAuthenticateService authenticateService)
        {
            _authenticateService = authenticateService;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> CreateToken([FromBody] TokenRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            string token;

            if(_authenticateService.IsAuthenticated(request, out token))
            {
                return Ok();
            }
            return BadRequest("Invalid request");
        }
    }
}