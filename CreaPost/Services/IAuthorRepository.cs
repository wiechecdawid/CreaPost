using CreaPost.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreaPost.Services
{
    public interface IAuthorRepository : IRepository<Author>
    {
        Author GetAuthorWithArticles(int id);
    }
}
