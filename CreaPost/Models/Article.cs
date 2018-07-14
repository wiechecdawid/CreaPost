using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreaPost.Models
{
    public class Article
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Body { get; set; }
        public virtual Author Author { get; set; }
        public int AuthorId { get; set; }
        public Area Area { get; set; }
    }
}
