using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreaPost.Models
{
    public class Author
    {
        public Author()
        {
            Articles = new HashSet<Article>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<Article> Articles { get; set; }
    }
}
