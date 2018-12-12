using CreaPost.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreaPost.ViewModels
{
    public class AuthorDetailViewModel
    {
        public Author Author { get; set; }
        public IEnumerable<Article> Articles  { get; set; }
    }
}
