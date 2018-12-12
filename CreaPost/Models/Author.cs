using System;
using System.Collections.Generic;
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

        private string _name;
        public int Id { get; set; }
        public string Name {
            get
            {
                return _name;
            }
            set
            {
                _name = User.FirstName + " " + User.LastName;
            } }
        public DateTime RegisterDate { get; set; }
        public StoreUser User { get; set; }
        public virtual IEnumerable<Article> Articles { get; set; }
    }
}
