using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CreaPost.Controllers
{
    public class AboutController : Controller
    {
        [Route("/About/Index")]
        public IActionResult About()
        {
            return View();
        }
    }
}