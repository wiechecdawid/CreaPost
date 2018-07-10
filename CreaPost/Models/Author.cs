using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreaPost.Models
{
    public class Author
    {
        private DateTime _lastVisited;

        public Author()
        {
            Articles = new HashSet<Article>();
            _lastVisited = DateTime.Now;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime RegisterDate { get; set; }
        public string LastVisited { get { return _lastVisited.ToString("g"); } }
        public IEnumerable<Article> Articles { get; set; }
    }
}
