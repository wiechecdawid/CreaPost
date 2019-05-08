using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CreaPost.Models
{
    public class StoreUser : IdentityUser
    {
        public StoreUser()
        {
            Articles = new HashSet<Article>();
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public IEnumerable<Article> Articles { get; set; }
        //public virtual Author Author { get; set; }
        //[ForeignKey("Author")]
        //public int AuthorId { get; set; }
    }
}
