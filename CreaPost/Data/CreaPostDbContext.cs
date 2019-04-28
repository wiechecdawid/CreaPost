using CreaPost.Data.EntitiesConfiguration;
using CreaPost.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreaPost.Data
{
    public class CreaPostDbContext : IdentityDbContext<StoreUser>
    {
        private DbContextOptions _options;

        public CreaPostDbContext(DbContextOptions<CreaPostDbContext> options)
            :base(options)
        {
            _options = options;
        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<StoreUser> StoreUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new ArticleConfiguration());

            //modelBuilder.Entity<StoreUser>()
            //    .HasOne(s => s.Author)
            //    .WithOne(a => a.User);
        }
    }
}
