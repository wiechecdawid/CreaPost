using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
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
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public DateTime RegisterDate { get; set; }
        //public virtual StoreUser User { get; set; }
        public virtual IEnumerable<Article> Articles { get; set; }
    }
}
