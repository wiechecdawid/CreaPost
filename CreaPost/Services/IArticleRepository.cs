using CreaPost.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreaPost.Services
{
    public interface IArticleRepository : IRepository<Article>
    {
        IEnumerable<Article> GetArticlesByAuthor(Author author);
        IEnumerable<Article> GetRecentArticles();
    }
}
