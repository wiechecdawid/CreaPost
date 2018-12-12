using CreaPost.Data;
using CreaPost.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreaPost.Services
{
    public class AuthorRepository : Repository<Author>, IAuthorRepository 
    {
        public AuthorRepository(CreaPostDbContext context) 
            : base(context)
        {
        }

        public Author GetAuthorWithArticles(int id)
        {
            return this.context.Authors.Include(a => a.Articles).SingleOrDefault(a => a.Id == id);
        }

        public override Author Get(int id)
        {
            return this.context.Authors.FirstOrDefault(a => a.Id == id);
        }
    }
}
