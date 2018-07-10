using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CreaPost.Data;
using CreaPost.Models;
using Microsoft.EntityFrameworkCore;

namespace CreaPost.Services
{
    public class ArticleRepository : Repository<Article>, IArticleRepository
    {
        public ArticleRepository(CreaPostDbContext context) 
            : base(context)
        {
        }
       
        public IEnumerable<Article> GetArticlesByAuthor(Author author)
        {
            throw new NotImplementedException();
        }
    }
}
