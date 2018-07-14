using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreaPost.Models
{
    public class Author
    {
        //private DateTime? _lastVisited;

        public Author()
        {
            Articles = new HashSet<Article>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime RegisterDate { get; set; }
        public virtual IEnumerable<Article> Articles { get; set; }
    }
}
