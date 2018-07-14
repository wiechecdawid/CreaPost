using CreaPost.Data.EntitiesConfiguration;
using CreaPost.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreaPost.Data
{
    public class CreaPostDbContext : DbContext
    {
        private DbContextOptions _options;

        public CreaPostDbContext(DbContextOptions<CreaPostDbContext> options)
            :base(options)
        {
            _options = options;
        }

        public DbSet<Author> Autors { get; set; }
        public DbSet<Article> Articles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ArticleConfiguration());
        }
    }
}
