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
            return this.context.Articles.Where(a => a.Author == author);
        }

        public IEnumerable<Article> GetRecentArticles()
        {
            return this.context.Articles.OrderByDescending(a => a.Id).Take(10).ToList();
        }
    }
}
