using CreaPost.Models;
using CreaPost.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreaPost.ViewModels
{
    public class HomeIndexViewModel
    {
        public IEnumerable<Article> Articles { get; set; }
        public IOwner Owner { get; set; }
    }
}
