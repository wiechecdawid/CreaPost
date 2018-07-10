using CreaPost.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreaPost.Services
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly CreaPostDbContext context;
        
        public Repository(CreaPostDbContext context)
        {
            this.context = context;
        }
        public TEntity Add(TEntity entity)
        {
            this.context.Set<TEntity>().Add(entity);
            return entity;
        }

        public virtual TEntity Get(int id)
        {
            return this.context.Set<TEntity>().Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return this.context.Set<TEntity>().ToList();
        }
    }
}
